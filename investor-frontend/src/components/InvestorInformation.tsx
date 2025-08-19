import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { InvestorDetail } from '../types/types';
import { investorApi } from '../services/apis';
import './InvestorInformation.css';

const InvestorInformation: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  const [investor, setInvestor] = useState<InvestorDetail | null>(null);
  const [assetClasses, setAssetClasses] = useState<string[]>([]);
  const [selectedAssetClass, setSelectedAssetClass] = useState<string>('');

  useEffect(() => {
    const fetchInvestorData = async () => {
      if (!id) return;
      
      try {
        const [investorData, assetClassesData] = await Promise.all([
          investorApi.getInvestor(parseInt(id)),
          investorApi.getAssetClasses()
        ]);
        
        setInvestor(investorData);
        setAssetClasses(assetClassesData);
      } catch (err) {
        console.error('Error fetching investor details:', err);
      }
    };
    fetchInvestorData();
  }, [id]);

  useEffect(() => {
    const fetchFilteredInvestor = async () => {
      if (!id || !selectedAssetClass) 
        return;
      
      try {
        const data = await investorApi.getInvestor(parseInt(id), selectedAssetClass);
        setInvestor(data);
      } catch (err) {
        console.error('Error fetching filtered commitments:', err);
      }
    };

    if (selectedAssetClass) {
      fetchFilteredInvestor();
    }
  }, [id, selectedAssetClass]);

  const formatCurrency = (amount: number) => {
    if (amount >= 1_000_000_000) {
      return `${(amount / 1_000_000_000).toFixed(2)}B`;
    } else if (amount >= 1_000_000) {
      return `${(amount / 1_000_000).toFixed(2)}M`;
    } else {
      return `${amount.toLocaleString('en-GB', { maximumFractionDigits: 0 })}`;
    }
  };

  // Calculate totals for each asset class
  const assetClassTotals: { [key: string]: number } = {};
  investor?.commitments.forEach((commitment) => {
    assetClassTotals[commitment.assetClass] =
      (assetClassTotals[commitment.assetClass] || 0) + commitment.amount;
  });

  const totalCommitments = investor?.commitments?.reduce((sum, commitment) => sum + commitment.amount, 0) || 0;
 
  if (!investor) {
    return <div>Loading investor information...</div>;
  }

  return (
    <div className="investor-detail-container">
      <div className="investor-detail-header">
        <h1>{investor.name}</h1>
      </div>

      <div className="filter-section">
        <nav className="asset-class-nav">
          <button
            className={`asset-class-btn${selectedAssetClass === '' ? ' selected' : ''}`}
            onClick={() => setSelectedAssetClass('')}
          >
            <span className="asset-class-label">All</span>
            <span className="asset-class-amount">{formatCurrency(totalCommitments)}</span>
          </button>
          {Object.entries(assetClassTotals).map(([assetClass, total]) => (
            <button
              key={assetClass}
              className={`asset-class-btn${selectedAssetClass === assetClass ? ' selected' : ''}`}
              onClick={() => setSelectedAssetClass(assetClass)}
            >
              <span className="asset-class-label">{assetClass}</span>
              <span className="asset-class-amount">{formatCurrency(total)}</span>
            </button>
          ))}
        </nav>
      </div>

      <div className="commitments-section">
        {investor.commitments?.length === 0 ? (
          <div className="no-commitments">
            <p>No commitments found for the selected criteria.</p>
          </div>
        ) : (
          <table className="commitments-table">
            <thead>
              <tr>
                <th>Id</th>
                <th>Asset Class</th>
                <th>Currency</th>
                <th>Amount</th>
              </tr>
            </thead>
            <tbody>
              {investor.commitments?.map((commitment) => (
                <tr key={commitment.id}>
                  <td>{commitment.id}</td>
                  <td>{commitment.assetClass}</td>
                  <td>{commitment.currency}</td>
                  <td>{formatCurrency(commitment.amount)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default InvestorInformation;
