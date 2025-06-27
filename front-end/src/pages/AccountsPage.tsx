import AccountsList from "../components/accounts/accountList";

const AccountsPage = () => {
  return (
    <div>
      <h1 className="text-xl font-semibold mb-4">Accounts</h1>
      {/* Replace below with <AccountForm /> and <AccountTable /> when ready */}
    <AccountsList/>
    </div>
  );
};

export default AccountsPage;
