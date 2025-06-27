import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/layout/Layout";
import AccountsPage from "./pages/AccountsPage";
import JournalEntryPage from "./pages/JournalPage";
import JournalListPage from "./pages/JournalsPage";
import TrialBalancePage from "./pages/TrialBalancePage";

const AppRoutes = () => {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Navigate to="/accounts" replace />} />
          <Route path="/accounts" element={<AccountsPage />} />
          <Route path="/journal-entry" element={<JournalEntryPage />} />
          <Route path="/journals" element={<JournalListPage />} />
          <Route path="/trial-balance" element={<TrialBalancePage />} />
          <Route path="*" element={<div className="p-4 text-red-600">404 - Page Not Found</div>} />
        </Routes>
      </Layout>
    </Router>
  );
};

export default AppRoutes;
