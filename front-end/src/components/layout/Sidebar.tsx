import { NavLink } from "react-router-dom";
import { Home, BookOpen, List, Calculator } from "lucide-react";

const Sidebar = () => {
  const navItems = [
    { name: "Accounts", path: "/accounts", icon: <Home size={18} /> },
    { name: "Journal Entry", path: "/journal-entry", icon: <BookOpen size={18} /> },
    { name: "Journals", path: "/journals", icon: <List size={18} /> },
    { name: "Trial Balance", path: "/trial-balance", icon: <Calculator size={18} /> },
  ];

  return (
    <aside className="w-64 h-screen bg-gray-900 text-white flex flex-col">
      <div className="p-4 text-xl font-bold border-b border-gray-700">
        Qtec Assessment
      </div>

      <nav className="flex-1 p-2 space-y-1">
        {navItems.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            className={({ isActive }) =>
              `flex items-center gap-3 px-4 py-2 rounded-md transition 
               ${isActive ? "bg-gray-800 font-semibold" : "hover:bg-gray-800"}`
            }
          >
            {item.icon}
            <span>{item.name}</span>
          </NavLink>
        ))}
      </nav>

      <div className="p-4 text-xs text-gray-400 border-t border-gray-700">
        © 2025 Ismail
      </div>
    </aside>
  );
};

export default Sidebar;
