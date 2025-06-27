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
    },
    journals: {
        list: baseUrl+"/journals",
        create: baseUrl+"/journals/create",
    },
    reports: {
        balanceSheet: baseUrl+"/reports/balance-sheet",
        trialBalance: baseUrl+"/reports/trial-balance"
    }
}