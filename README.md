# OneTravel

## Contents Submitted

* **Flight Data Generator** (`FlightDataGenerator.sql`)
* **OneTravel Project Solution**
  ├── **Server** (ASP.NET Core)
  │   ├── *Controllers*/
  │   ├── *DataAccess*/
  │   ├── *Models*/
  │   ├── *Properties*/
  │   └── `Program.cs`
  └── **Client** (React + TypeScript)
     ├── *models*/
     ├── *pages*/
     ├── *redux*/
     ├── *router*/
     └── *styles*/

---

## Getting Started

### Prerequisites

| Tool                | Minimum Version | Purpose                           |
| ------------------- | --------------- | --------------------------------- |
| .NET SDK            | 8.0 (LTS)       | Build & run the ASP.NET backend   |
| Node.js & npm       | 20.x            | Build & run the React client      |
| Visual Studio 2022  | ‑               | Recommended IDE for the server    |
| VS Code             | ‑               | Recommended editor for the client |
| SQL Server          | 2019+           | Sample flight‑data database       |
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
   2. Press F5 or click **Run**.
   3. The API listens on `https://localhost:5001` (default).

3. **Start the client** (VS Code or terminal)

   ```bash
   cd Client
   npm install   # first‑time only
   npm start
   ```

   The React app launches at `http://localhost:3000` and proxies API requests to the backend.

4. **(Optional) Seed sample data**

   * Execute `FlightDataGenerator.sql` against your SQL Server instance to populate demo flight records.

---

## Known Limitations

* The free‑tier flight‑data API enforces request quotas and offers limited coverage. A commercial subscription would provide richer, real‑time data.

---

## Deliverables

| Item               | Location                                                         |
| ------------------ | ---------------------------------------------------------------- |
| **Project Report** | `/docs/OneTravel_Report.pdf`                                     |
| **Source Package** | `OneTravel.zip` (client, server, SQL, models, experimental data) |
| **This README**    | `README.md`                                                      |
