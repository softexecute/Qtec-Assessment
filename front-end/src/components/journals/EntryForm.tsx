import { useEffect, useState } from "react";
import Select from "react-select";
import { Fragment } from "react/jsx-runtime"
import { EndPoints } from "../../services/accountsType";
const JournalEntryForm = () => {
    const [lines, setLines] = useState([
        { accountId: "", debit: "", credit: "" },
    ]);
    const [date, setDate] = useState("");
    const [description, setDescription] = useState("");
    const [accountOptions, setAccountOptions] = useState([]);


    const handleLineChange = (index: number, field: string, value: string) => {
        const updated = [...lines];
        updated[index] = { ...updated[index], [field]: value };
        setLines(updated);
    };

    const addLine = () => {
        setLines([...lines, { accountId: "", debit: "", credit: "" }]);
    };


    const RemoveLines = (index: number) => {
        const updatedLines = lines.filter((_, i) => i !== index);
        console.log("Updated Lines", updatedLines);
        setLines(updatedLines);
    };

    const totalDebit = lines.reduce(
        (sum, l) => sum + (parseFloat(l.debit) || 0),
        0
    );
    const totalCredit = lines.reduce(
        (sum, l) => sum + (parseFloat(l.credit) || 0),
        0
    );
    const handleSubmit = async () => {
        if (lines.length < 1) {
            alert("Please add at least one line item.");
            return;
        }

        else if (totalDebit !== totalCredit) {
            alert("Total debit must equal total credit.");
            return;
        }

        else {
            const entry = {
                date,
                description,
                lines: lines.map((line) => ({
                    //@ts-ignore
                    accountId: line.accountId?.value, // Assuming accountId is an object with a value property
                    debit: parseFloat(line.debit) || 0,
                    credit: parseFloat(line.credit) || 0,
                })),
            };

            let response = await fetch(EndPoints.journals.create, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(entry),
            });
            if (response.ok) {
                alert("Journal entry created successfully!");
                // Reset form
                setLines([{ accountId: "", debit: "", credit: "" }]);
                setDate("");
                setDescription("");
            }
        }


    };

    // Filter out selected accounts to avoid duplicate selection
    const getAvailableOptions = (index: number) => {
        const selectedValues = lines
            .filter((_, i) => i !== index)
            //@ts-ignore
            .map((l) => l.accountId?.value);
        return accountOptions.filter((opt: any) => !selectedValues.includes(opt.value));
    };


    useEffect(() => {
        loadAccounts();
    }, [])


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
            // Map 'id' to 'value' and 'type' to 'label'
            const mappedOptions = data.map((account: any) => ({
                value: account.id,
                label: account.name
            }));
            setAccountOptions(mappedOptions);
        } catch (error) {
            console.error("Error loading accounts:", error);
        }
    };


    const isEqual = (Number(totalDebit.toFixed(2)) === Number(totalCredit.toFixed(2))) && Number(totalDebit.toFixed(2)) > 0;




    return (
        <Fragment>
            <div className="max-w-4xl px-6 py-1 bg-white shadow-md rounded" >
                <h1 className="text-xl font-semibold mb-4">Journal Entry</h1>
                <div className="mb-4">
                    <label className="block mb-1 font-medium">Date</label>
                    <input
                        type="date"
                        value={date}
                        onChange={(e) => setDate(e.target.value)}
                        className="w-4/12 border px-3 py-2 rounded"
                        required
                    />
                </div>

                <div className="mb-4">
                    <label className="block mb-1 font-medium">Description</label>
                    <input
                        type="text"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        className="w-full border px-3 py-2 rounded"
                        placeholder="Enter description"
                        required
                    />
                </div>

                <div className="mb-4">
                    <table className="w-full table-auto">
                        <thead>
                            <tr className="bg-gray-100 text-left">
                                <th className="p-2 border w-10">#</th>
                                <th className="p-2 border">Account</th>
                                <th className="p-2 border w-40">Debit</th>
                                <th className="p-2 border w-40">Credit</th>
                                <th className="p-2 border w-20"></th>
                            </tr>
                        </thead>
                        <tbody>
                            {lines.map((line, index) => (
                                <tr key={index}>
                                    <td className="p-2 text-center">{index + 1}</td>
                                    <td className="p-2">
                                        <Select
                                            value={line.accountId}
                                            onChange={(selected: any) =>
                                                handleLineChange(index, "accountId", selected)
                                            }
                                            options={getAvailableOptions(index)}
                                            placeholder="Select account"
                                            isClearable
                                        />
                                    </td>
                                    <td className="p-2">
                                        <input
                                            type="number"
                                            value={line.debit}
                                            onChange={(e) =>
                                                handleLineChange(index, "debit", e.target.value)
                                            }
                                            className="w-full rounded px-2 py-1"
                                            placeholder="0.00"
                                            step="0.01"
                                        />
                                    </td>
                                    <td className="p-2">
                                        <input
                                            type="number"
                                            value={line.credit}
                                            onChange={(e) =>
                                                handleLineChange(index, "credit", e.target.value)
                                            }
                                            className="w-full rounded px-2 py-1"
                                            placeholder="0.00"
                                            step="0.01"
                                        />
                                    </td>
                                    <td className="text-center">
                                        <button className="bg-red-400 px-2 py-2 rounded cursor-pointer text-white" type="button" onClick={() => RemoveLines(index)}>&times;</button>
                                    </td>
                                </tr>

                            ))}
                        </tbody>
                        <tfoot>
                            <tr>
                                <td></td>
                                <td className="text-end">Total</td>
                                <td className="text-end"><span> {totalDebit.toFixed(2)}</span></td>
                                <td className="text-end"> <span> {totalCredit.toFixed(2)}</span></td>
                            </tr>
                        </tfoot>
                    </table>

                    <button
                        type="button"
                        onClick={addLine}
                        className="mt-3 px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                    >
                        + Add Line
                    </button>
                </div>
                <div className="clearfix text-end">
                    <button
                        type="button"
                        onClick={handleSubmit}
                        className="w-2/12 py-2 rounded 
                                    text-white 
                                    bg-blue-600 
                                    hover:bg-blue-700 
                                    disabled:bg-gray-400 
                                    disabled:cursor-not-allowed"
                        disabled={!isEqual || lines.length < 1}
                    >
                        Save Transaction
                    </button>
                </div>
            </div>
        </Fragment>
    )
}

export default JournalEntryForm;