import React, { useState, useEffect } from "react";
import TableView from "../components/common/TableView/TableView";

interface Investor {
  id: string;
  name: string;
  investorType: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  totalCommitments: number;
}

interface FormattedInvestor extends Omit<Investor, "totalCommitments"> {
  totalCommitments: string;
}

const formatCurrency = (value: number): string => {
  const billions = value / 1e9;
  return `${billions.toFixed(2)}B`;
};

const apiUrl = process.env.REACT_APP_API_URL;

const InvestorsPage: React.FC = () => {
  const [investorsData, setInvestorsData] = useState<FormattedInvestor[]>([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const rowsPerPage = 10;


  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${apiUrl}/api/investors?pageIndex=${currentPage}&pageSize=${rowsPerPage}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        const formattedData: FormattedInvestor[] = data.items.map((item: Investor) => ({
          ...item,
          dateAdded : item.dateAdded.split('T')[0],
          lastUpdated: item.lastUpdated.split('T')[0],
          totalCommitments: formatCurrency(item.totalCommitments),
        }));
        setInvestorsData(formattedData);
        setTotalItems(data.totalCount);
        setTotalPages(data.totalPages);
      } catch (error) {
        console.error("Failed to fetch investors:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [currentPage, rowsPerPage]);

  const columns = [
    { field: "id", headerName: "ID" },
    { field: "name", headerName: "Name" },
    { field: "investorType", headerName: "Investor Type" },
    { field: "country", headerName: "Country" },
    { field: "dateAdded", headerName: "Date Added" },
    { field: "lastUpdated", headerName: "Last Updated" },
    { field: "totalCommitments", headerName: "Total Commitments" },
  ];

  const linkColumn = {
    field: "name",
    getUrl: (row: Investor) => `/investor/${row.id}`,
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!loading && investorsData.length === 0) {
    return <div>No data available.</div>;
  }

  return (
    <div className="container mt-4">
      <h2>Investors</h2>
      <TableView
        columns={columns}
        data={investorsData}
        linkColumn={linkColumn}
        rowsPerPage={rowsPerPage}
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        totalItems={totalItems}
        totalPages={totalPages}
      />
    </div>
  );
};

export default InvestorsPage;
