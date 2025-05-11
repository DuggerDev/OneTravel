# OneTravel

## Contents Submitted

* **Flight Data Generator** (`fake_travel.py`)
* **Data Normalizer** (`Mormalize.sql`)

```md
OneTravel Project Solution
  ├── Server (ASP.NET Core)
  │   ├── Controllers/
  │   ├── DataAccess/
  │   ├── Models/
  │   ├── Properties/
  │   └── Program.cs
  └── Client (Vite + TypeScript)
     ├── models/
     ├── pages/
     ├── redux/
     ├── router/
     └── styles/
```

## Getting Started

### Prerequisites

| Tool                | Minimum Version | Purpose                           |
| ------------------- | --------------- | --------------------------------- |
| .NET SDK            | 8.0 (LTS)       | Build & run the ASP.NET backend   |
| Node.js & npm       | 20.x            | Build & run the React client      |
| Visual Studio 2022  | ‑               | Recommended IDE for the server    |
| VS Code             | ‑               | Recommended editor for the client |
| SQLite              | 3               | Sample flight‑data database       |
| Modern browser      | ‑               | Access the web app                |
| Internet connection | ‑               | External API calls                |

> **Tip** If you use different versions, ensure they are compatible with the ones listed above.

### Running the Application

1. **Clone & open the solution**

   ```bash
   git clone <your‑fork‑url>
   cd OneTravel
   ```

2. **Start the server** (Visual Studio)

   1. Open `OneTravel.sln`.
   2. Press CTRL+F5 or click **Run without debugging**.
   3. The API listens on `https://localhost:7212` (default).
   4. Vite application also launches at `http://localhost:5173` (default).

---

## Known Limitations

* The free‑tier flight‑data API enforces request quotas and offers limited coverage. A commercial subscription would provide richer, real‑time data.
* The SQLite database is a sample and not intended for production use, data would need to be regenerated using the `fake_travel.py` script and Normalize it using the `Normalize.sql` script.
* Can only search for flights or hotels, but not purchase the tickets.

---

## Deliverables

| Item               | Location                                                         |
| ------------------ | ---------------------------------------------------------------- |
| **Project Report** | `/docs/OneTravel_Report.pdf`                                     |
| **Source Package** | `OneTravel.zip` (client, server, SQL, models, experimental data) |
| **This README**    | `README.md`                                                      |
