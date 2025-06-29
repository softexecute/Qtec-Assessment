import { useEffect, useState } from "react";
import Select from "react-select/base";
import { Fragment } from "react/jsx-runtime"
import { AccountTypes, EndPoints } from "../../services/accountsType";
import { data } from "react-router-dom";
import { Account } from "../../types/account";


const AccountsList = () => {
    const [accounts, setAccounts] = useState([]);
    const [isNewMode, setIsNewMode] = useState(false);
    const [accountName, setAccountName] = useState("");
    const [accountType, setAccountType] = useState(""); // update this from <select> or react-select

    useEffect(() => {
        loadAccounts();
    }, []);

    const loadAccounts = async () => {
        try {
            const response = await fetch(EndPoints.accounts.list, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            });

            if (!response.ok) {
                throw new Error("Failed to fetch accounts");
            }

            const data = await response.json();
            setAccounts(data);
        } catch (error) {
            console.error("Error loading accounts:", error);
        }
    };



    const handleSave = async () => {
        if (accountName.trim() === "" || !accountType) { }
        const newAccount = {
            type: accountType,
            name: accountName
        };

        var response = await fetch(EndPoints.accounts.create, {
            method: "POST",
            body: JSON.stringify(newAccount),
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.ok) {

            loadAccounts();
            setAccountName("");
            setAccountType("");
            setIsNewMode(false);
        }
        if (!response.ok) {
            const errorData = await response.json();

            // Check for validation errors
            if (errorData.errors) {
                for (const [field, messages] of Object.entries(errorData.errors)) {
                    alert(`${field}: ${(messages as any).join(", ")}`);
                }
            }

            else if (errorData.message) {
                alert(`Error: ${errorData.message}`);
            }

            else {
                console.error("Unexpected error", errorData);
            }

            return;
        }

    }



    const handleTypeChange = (event) => {
        setAccountType(event.target.value);
    }

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this account?")) {
            try {
                const response = await fetch(`${EndPoints.accounts.delete}/${id}`, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json"
                    }
                });

                if (!response.ok) {
                    var errorResponse = await response.json();
                    if (errorResponse.message) {
                        alert(`Error: ${errorResponse.message}`);
                    }
                }

                // Reload accounts after deletion
                loadAccounts();
            } catch (error) {
                console.error("Error deleting account:", error);

            }
        }
    }


    return (
        <Fragment>
            <div className="overflow-x-auto">
                <table className="min-w-full border border-gray-200 text-sm text-left text-gray-700">
                    <thead className="bg-gray-100 text-xs uppercase text-gray-600">
                        <tr>
                            <th className="px-4 py-2 border-b">SL</th>
                            <th className="px-4 py-2 border-b">Type</th>
                            <th className="px-4 py-2 border-b">Account Name</th>
                            <th className="px-4 py-2 border-b"></th>
                        </tr>
                    </thead>
                    <tbody>
                        {accounts.map((account: Account, index) => (
                            <tr key={index} className="hover:bg-gray-50">
                                <td className="px-4 py-2 border-b">{index + 1}</td>
                                <td className="px-4 py-2 border-b">{account.type}</td>
                                <td className="px-4 py-2 border-b">{account.name}</td>
                                <td className="px-4 py-2 border-b">
                                    <button onClick={() => handleDelete(account.id)} className="p-2 bg-red-500 text-white rounded hover:bg-white-600 hover:text-black disabled:opacity-50"> Delete </button>
                                </td>
                            </tr>
                        ))}
                        {accounts.length === 0 && (
                            <tr>
                                <td colSpan={3} className="text-center py-4">Total Accounts : {accounts.length}</td>
                            </tr>
                        )}
                    </tbody>

                    <tfoot>

                        {isNewMode && (
                            <tr className="text-sm text-gray-700">
                                <td></td>
                                <td className="px-4 py-2">
                                    <select onChange={handleTypeChange}
                                        name="type"
                                        className="w-full px-2 py-1 border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                                    >
                                        <option value="">Select Type</option>
                                        {AccountTypes.map((option, index) => (
                                            <option key={index} value={option.value}>
                                                {option.label}
                                            </option>
                                        ))}
                                    </select>
                                </td>
                                <td className="px-4 py-2">
                                    <div className="flex items-center gap-2">
                                        <input
                                            type="text"
                                            name="accountsName"
                                            value={accountName}
                                            onChange={(e) => setAccountName(e.target.value)}
                                            placeholder="Account Name"
                                            className="w-full px-2 py-1 border border-gray-300 rounded focus:outline-none focus:ring-1 focus:ring-blue-500"
                                        />

                                        {accountType && accountName.trim() !== "" && (
                                            <button
                                                onClick={handleSave}
                                                className="p-2 bg-green-500 text-white rounded hover:bg-green-600 disabled:opacity-50"
                                            >
                                                Save
                                            </button>
                                        )}
                                    </div>
                                </td>

                            </tr>
                        )}

                        <tr>
                            <td colSpan={3} className="text-center py-4">
                                <button
                                    onClick={() => setIsNewMode(!isNewMode)}
                                    className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                                >
                                    {isNewMode ? "Cancel" : "Add New Account"}
                                </button>
                            </td>

                        </tr>
                    </tfoot>
                </table>

            </div>
        </Fragment>
    );
}
export default AccountsList;