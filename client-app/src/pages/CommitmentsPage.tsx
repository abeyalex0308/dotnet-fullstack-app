import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import TableView from "../components/common/TableView/TableView";
import AssetSummary from "../components/AssetSummary/AssetSummary";

interface Commitment {
  id: string;
  asset: string;
  currency: string;
  amount: number;
}

interface AssetData {
  assetClass: string;
  asset: string;
  totalCommitments: number;
}

const MAPPINGS: { [key: string]: string } = {
  All: "All",
  Infrastructure: "Infrastructure",
  HedgeFunds: "Hedge Funds",
  PrivateEquity: "Private Equity",
  NaturalResources: "Natural Resources",
  PrivateDebt: "Private Debt",
  RealEstate: "Real Estate",
};

const formatCurrency = (value: number): string => {
  const millions = value / 1e6;
  return `${millions.toFixed(2)}M`;
};
const apiUrl = process.env.REACT_APP_API_URL;

const Commitments: React.FC = () => {
  const { investorId } = useParams<{ investorId: string }>();
  const [commitmentsData, setCommitmentsData] = useState<Commitment[]>([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalItems, setTotalItems] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [assetData, setAssetData] = useState<AssetData[]>([]);
  const [selectedAssetClass, setSelectedAssetClass] = useState<string>("");
  const rowsPerPage = 10;

 

  useEffect(() => {
    const fetchCommitmentsData = async (assetId: string | null = null) => {
      setLoading(true);
      try {
        let url = `${apiUrl}/api/investors/${investorId}/commitments?pageIndex=${currentPage}&pageSize=${rowsPerPage}`;
        url += assetId ? `&assetClass=${assetId}` : "";
        const response = await fetch(url);
        if (!response.ok) throw new Error("Error fetching commitments data");
        const data = await response.json();
        const formattedData: Commitment[] = data.items.map((item: Commitment) => ({
          ...item,
          asset: MAPPINGS[item.asset],
        }));
        setCommitmentsData(formattedData);
        setTotalItems(data.totalCount);
        setTotalPages(data.totalPages);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    fetchCommitmentsData(selectedAssetClass);
  }, [investorId, currentPage, selectedAssetClass]);

  useEffect(() => {
    const fetchAssetData = async () => {
      try {
        const response = await fetch(`${apiUrl}/api/investors/${investorId}/commitments/totalByAssetClass`);
        if (!response.ok) throw new Error("Error fetching asset data");
        const data = await response.json();
        const total = data.reduce((acc: number, item: AssetData) => acc + item.totalCommitments, 0);
        const formattedData: AssetData[] = data.map((item: AssetData) => ({
          ...item,
          asset: MAPPINGS[item.asset],
        }));

        setAssetData([{ assetClass: "", asset: "All", totalCommitments: total }, ...formattedData]);
      } catch (error) {
        console.error(error);
      }
    };
    fetchAssetData();
  }, []);

  const columns = [
    { field: "id", headerName: "ID" },
    { field: "asset", headerName: "Asset Class" },
    { field: "currency", headerName: "Currency" },
    { field: "amount", headerName: "Amount" },
  ];

  const handleAssetClick = (assetClass: string) => {
    setSelectedAssetClass(assetClass);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container mt-4">
      <h2>Commitments</h2>
      <AssetSummary assetData={assetData} selectedAssetClass={selectedAssetClass} onAssetClick={handleAssetClick} />
      <TableView
        columns={columns}
        data={commitmentsData.map((commitment) => ({
          ...commitment,
          amount: formatCurrency(commitment.amount),
        }))}
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        totalItems={totalItems}
        totalPages={totalPages}
        rowsPerPage={rowsPerPage}
      />
    </div>
  );
};

export default Commitments;
