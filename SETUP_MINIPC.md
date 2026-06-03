# Setup Mini PC — SQL Server + Migrations
# (rede via Tailscale)

## 1. Mini PC (Ubuntu Server) — instalar Docker

```bash
curl -fsSL https://get.docker.com | sh
sudo usermod -aG docker $USER
newgrp docker
```

---

## 2. Instalar Tailscale no Mini PC

```bash
curl -fsSL https://tailscale.com/install.sh | sh
sudo tailscale up
```

Autenticar no browser quando pedido. Depois:

```bash
# ver o IP Tailscale do mini PC (gama 100.x.x.x)
tailscale ip -4
```

> Guardar esse IP — é o que vais usar na connection string da tua máquina de dev.
> Este IP nunca muda enquanto o dispositivo estiver na tua rede Tailscale.

---

## 3. SQL Server em Docker

```bash
mkdir -p /opt/clinicsoft
nano /opt/clinicsoft/docker-compose.yml
```

```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: clinicsoft-db
    environment:
      SA_PASSWORD: "StrongPassword123!"
      ACCEPT_EULA: "Y"
      MSSQL_MEMORY_LIMIT_MB: 4096
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    restart: unless-stopped

volumes:
  sqldata:
```

```bash
cd /opt/clinicsoft
docker compose up -d

# verificar
docker ps
docker logs clinicsoft-db
```

---

## 4. Firewall — só permitir via Tailscale

O Tailscale usa a interface `tailscale0`. Bloqueia o acesso externo e permite apenas pelo Tailscale:

```bash
# permitir tudo via Tailscale
sudo ufw allow in on tailscale0

# garantir que a porta 1433 NÃO está aberta para a internet
sudo ufw deny 1433/tcp

sudo ufw reload
sudo ufw status
```

> Assim o SQL Server só é acessível por dispositivos na tua rede Tailscale — mais seguro.

---

## 5. Copiar o Projecto para o Mini PC

Via Tailscale:

```bash
scp -r /home/rick_sanchez/Documents/Projects/ClinicSoftApp rick-sanchez@100.79.173.10:~/Projects/
```

Ou via git (recomendado):

```bash
# no mini pc
git clone <url-do-repo> ~/Projects/ClinicSoftApp
```

---

## 6. Instalar .NET 10 no Mini PC

```bash
wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install -y dotnet-sdk-10.0

dotnet --version
```

---

## 7. Instalar EF Core Tools

```bash
dotnet tool install --global dotnet-ef
echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
source ~/.bashrc

dotnet ef --version
```

---

## 8. Connection Strings

**No mini PC** — `appsettings.Development.json` na pasta da WebAPI:
```json
{
  "ConnectionStrings": {
    "ClinicSoftCS": "Server=localhost,1433;Database=ClinicSoftDB;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;"
  }
}
```

**Na tua máquina de dev** — usa o IP Tailscale do mini PC:
```json
{
  "ConnectionStrings": {
    "ClinicSoftCS": "Server=100.79.173.10,1433;Database=ClinicSoftDB;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;"
  }
}
```

---

## 9. Correr as Migrations (no Mini PC)

```bash
cd ~/Projects/ClinicSoftApp

# criar a primeira migration (após FASE 5 — WebAPI montada)
dotnet ef migrations add InitialCreate \
  --project ClinicSoft.Data \
  --startup-project ClinicSoft.WebAPI

# aplicar ao banco de dados
dotnet ef database update \
  --project ClinicSoft.Data \
  --startup-project ClinicSoft.WebAPI
```

---

## 10. Verificar Base de Dados

Ligar com Azure Data Studio ou DBeaver **na tua máquina de dev**:

- **Host:** `100.79.173.10`
- **Port:** `1433`
- **User:** `sa`
- **Password:** `StrongPassword123!`
- **Database:** `ClinicSoftDB`

---

## Estado do Projecto

| Fase | Descrição | Estado |
|------|-----------|--------|
| 1 | Domain.Core | ✅ |
| 2 | Domain | ✅ |
| 3 | Application (CQRS + MediatR) | ✅ |
| 4 | Data (DbContext + Repositórios) | ✅ |
| 5 | Security + IoC + WebAPI | ⏳ |

> As migrations só podem ser criadas depois da FASE 5 (WebAPI como startup project).

---

## Notas

- O IP Tailscale (`100.x.x.x`) é estável — não muda entre sessões
- Funciona mesmo fora de casa (Tailscale é uma VPN)
- O volume Docker `sqldata` persiste os dados mesmo que o container seja recriado
- `restart: unless-stopped` faz o SQL Server arrancar automaticamente com o mini PC
- Mudar a password `StrongPassword123!` para algo mais seguro
