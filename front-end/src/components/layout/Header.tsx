const Header = () => {
  return (
    <header className="bg-white border-b border-gray-200 px-6 py-3 shadow-sm">
      <div className="flex items-center justify-between">
        <h3 className="text-md font-semibold text-gray-800">
         A simple Accounting System
        </h3>
        <div className="text-sm text-gray-500">
          {/* You can replace with actual user name or dropdown later */}
         There is no login System  right now
        </div>
      </div>
    </header>
  );
};

export default Header;
