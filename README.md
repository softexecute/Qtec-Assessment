# 🧾 Simple Accounting Ledger Management System

A full-stack accounting journal management system built with **ASP.NET Core**, **Entity Framework Core**, and **React (Vite)**. The system supports journal entries, trial balance, account management, and more — ideal for small to medium financial record-keeping.

---

## 📂 Project Structure
Backend/ → ASP.NET Core Web API
Frontend/ → React + Vite UI
Database/ → SQL Server (code-first or script-based)


---

## ⚙️ Features

- 🔐 Secure journal entry system
- 📚 Account type management
- 💹 Trial balance report with accurate DR/CR calculation
- 📥 Stored procedure-based data handling
- 📊 Responsive and intuitive UI (React + TailwindCSS)

---

## 🚀 Setup Instructions

### 🛠️ Backend Setup (.NET Core)

#### Option 1: Create DB using Script

1. Open **SQL Server Management Studio (SSMS)**.
2. Execute the provided SQL script from the `Database/` folder.
3. Be sure to **manually run the stored procedure section** included in the script.

#### Option 2: Code First (EF Core)

1. Open the `Backend/` project in **Visual Studio**.
2. Update the `appsettings.json` connection string:

   ```json
   "DefaultConnection": "Server=YOUR_SERVER;Database=JournalDB;Trusted_Connection=True;"

2. Run the following in Package Manager Console:
```bash
Update-Database -Verbose
```
3. Start the backend project and copy the base URL (e.g. https://localhost:5142).


💻 Frontend Setup (React + Vite)
1. Open the Frontend/ project in VS Code.

2. Navigate to the Endpoints.ts file:
src/services-> Endpoints.ts
3. Update the base API URL like this:
 const baseURL = "https://localhost:5142/api";
4. Install dependencies and start dev server:
```bash
npm install
npm run dev
```
5. Copy the dev URL (e.g., http://localhost:5173) and open it in your browser.


# Thanks for being here
