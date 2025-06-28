import { Fragment, useEffect, useState } from "react";
import { EndPoints } from "../../services/accountsType";

type JournalEntry = {
  id: number;
  date: string;
  description: string;
  totalAmount: number;
};

type JournalLine = {
  accountName: string;
  debit: number;
  credit: number;
};

type JournalDetails = {
  journalId: number;
  date: string;
  description: string;
  lines: JournalLine[];
};

const JournalList = () => {
  const [journals, setJournals] = useState<JournalEntry[]>([]);
  const [selectedJournal, setSelectedJournal] = useState<JournalDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchJournals();
  }, []);

  const fetchJournals = async () => {
    try {
      const response = await fetch(EndPoints.journals.list);
      if (!response.ok) throw new Error("Failed to fetch journal entries");
      const data = await response.json();
      setJournals(data);
    } catch (err) {
      setError("Error loading journal");
    } finally {
      setLoading(false);
    }
  };

  const fetchJournalDetails = async (id: number) => {
    try {
      const response = await fetch(`${EndPoints.journals.get}/${id}`);
      if (!response.ok) throw new Error("Failed to fetch details");
      const data = await response.json();
      setSelectedJournal(data);
      setModalOpen(true);
    } catch (err) {
      console.error("Error loading journal details:", err);
    }
  };

  return (
    <Fragment>
      <div className="max-w-full px-4">
        <h1 className="text-2xl font-bold mb-4">Journal Entries</h1>

        {loading ? (
          <p>Loading...</p>
        ) : error ? (
          <p className="text-red-500">{error}</p>
        ) : (
          <div className="overflow-x-auto shadow border rounded">
            <table className="min-w-full bg-white text-sm">
              <thead>
                <tr className="bg-gray-100 text-left">
                  <th className="p-3 border-b">#</th>
                  <th className="p-3 border-b">Date</th>
                  <th className="p-3 border-b">Description</th>
                  <th className="p-3 border-b text-right">Transactional Amount</th>
                </tr>
              </thead>
              <tbody>
                {journals.map((entry, index) => (
                  <tr
                    key={entry.id}
                    onClick={() => fetchJournalDetails(entry.id)}
                    className="hover:bg-gray-50 cursor-pointer"
                  >
                    <td className="p-3 border-b">{index + 1}</td>
                    <td className="p-3 border-b">{new Date(entry.date).toLocaleDateString()}</td>
                    <td className="p-3 border-b">{entry.description}</td>
                    <td className="p-3 border-b text-right">{entry.totalAmount.toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {/* Modal */}
      {modalOpen && selectedJournal && (
        <div className="fixed inset-0 bg-black bg-opacity-25 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg shadow-lg w-full max-w-2xl p-6 relative">
            <button
              onClick={() => setModalOpen(false)}
              className="absolute top-2 right-2 text-gray-500 hover:text-black"
            >
              ✕
            </button>

            <h2 className="text-xl font-semibold mb-2">Journal #{selectedJournal.journalId}</h2>
            <p className="text-sm text-gray-600 mb-4">
              {new Date(selectedJournal.date).toLocaleDateString()} -{" "}
              {selectedJournal.description}
            </p>

            <table className="w-full text-sm border">
              <thead>
                <tr className="bg-gray-100">
                  <th className="border p-2">Account</th>
                  <th className="border p-2 text-right">Debit</th>
                  <th className="border p-2 text-right">Credit</th>
                </tr>
              </thead>
              <tbody>
                {selectedJournal.lines.map((line, index) => (
                  <tr key={index}>
                    <td className="border p-2">{line.accountName}</td>
                    <td className="border p-2 text-right">{line.debit.toFixed(2)}</td>
                    <td className="border p-2 text-right">{line.credit.toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
            </table>

            <div className="text-end mt-4">
              <button
                onClick={() => setModalOpen(false)}
                className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
              >
                Close
              </button>
            </div>
          </div>
        </div>
      )}
    </Fragment>
  );
};

export default JournalList;
