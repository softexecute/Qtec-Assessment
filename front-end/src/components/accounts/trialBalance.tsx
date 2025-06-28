import { useEffect, useState } from "react";
import { EndPoints } from "../../services/accountsType";

const TrialBalance = () => {
  const [trialBalance, setTrialBalance] = useState([]);
  const [loading, setLoading] = useState(true);

  const loadTrialBalance = async () => {
    try {
      const response = await fetch(EndPoints.accounts.trial_balance, {
        method: "GET",
        headers: {
          "Content-Type": "application/json"
        }
      });

      if (!response.ok) throw new Error("Failed to load trial balance");

      const data = await response.json();
      setTrialBalance(data);
    } catch (error) {
      console.error("Error loading trial balance:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadTrialBalance();
  }, []);

  return (
    <div className="max-w-full px-4">
      <h1 className="text-2xl font-bold mb-4">Trial Balance</h1>

      {loading ? (
        <p className="text-gray-600">Loading...</p>
      ) : (
        <div className="overflow-x-auto">
          <table className="min-w-full bg-white border border-gray-300">
            <thead className="bg-gray-100">
              <tr>
                <th className="px-4 py-2 border">ID</th>
                <th className="px-4 py-2 border">Account</th>
                <th className="px-4 py-2 border">Type</th>
                <th className="px-4 py-2 border text-right">Debit</th>
                <th className="px-4 py-2 border text-right">Credit</th>
                <th className="px-4 py-2 border text-right">Balance</th>
              </tr>
            </thead>
            <tbody>
              {trialBalance.map((item: any) => (
                <tr key={item.accountId}>
                  <td className="px-4 py-2 border text-center">{item.id}</td>
                  <td className="px-4 py-2 border">{item.account}</td>
                  <td className="px-4 py-2 border">{item.type}</td>
                  <td className="px-4 py-2 border text-right">{item.debit?.toFixed(2)}</td>
                  <td className="px-4 py-2 border text-right">{item.credit?.toFixed(2)}</td>
                  <td className="px-4 py-2 border text-right">{item.balance?.toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default TrialBalance;
