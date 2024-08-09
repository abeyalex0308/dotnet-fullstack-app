import React from 'react';
import './AssetSummary.css';

interface AssetDataItem {
  assetClass: string;
  asset: string;
  totalCommitments: number;
}

interface AssetSummaryProps {
  assetData: AssetDataItem[];
  selectedAssetClass: string | null;
  onAssetClick: (assetClass: string) => void;
}

export const formatCurrency = (value: number): string => {
  const millions = value / 1e9;
  return `£${millions.toFixed(2)}B`;
};

const AssetSummary: React.FC<AssetSummaryProps> = ({ assetData, selectedAssetClass, onAssetClick }) => {
  return (
    <div className="d-flex flex-wrap justify-content-start">
      {assetData.map((asset) => (
        <div
          key={asset.assetClass}
          className={`card asset-summary-card ${selectedAssetClass === asset.assetClass ? 'bg-primary text-white' : ''}`}
          onClick={() => onAssetClick(asset.assetClass)}
        >
          <div className="card-body">
            <p className="card-title">{asset.asset}</p>
            <p className="card-text">{formatCurrency(asset.totalCommitments)}</p>
          </div>
        </div>
      ))}
    </div>
  );
};

export default AssetSummary;
