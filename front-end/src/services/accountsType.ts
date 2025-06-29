export const AccountTypes = [
    { value: "Asset", label: "Asset" },
    { value: "Liability", label: "Liability" },
    { value: "Equity", label: "Equity" },
    { value: "Revenue", label: "Revenue" },
    { value: "Expense", label: "Expense" },
];


// EndPoints.ts
const baseUrl = "http://localhost:5142/api"; // Adjust the base URL as needed
export const EndPoints: any = {
    accounts: {
        list: baseUrl+"/accounts", // Get Request
        create: baseUrl+"/accounts", // Post Request
        trial_balance: baseUrl+"/accounts/trial-balance", // Get Request
        delete: baseUrl+"/accounts", // Delete Request, use with ID, e.g., /accounts/1
    },
    journals: {
        list: baseUrl+"/journal",
        create: baseUrl+"/journal",
        get: baseUrl+"/journal", // Use with ID, e.g., /journal/1
    },
}