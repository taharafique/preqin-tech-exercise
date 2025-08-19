import React, { useState, useEffect } from 'react';
import { Investor } from '../types/types';
import { investorApi } from '../services/apis';
import './InvestorsList.css';

const InvestorsList: React.FC = () => {
  const [investors, setInvestors] = useState<Investor[]>([]);

  useEffect(() => {
    const fetchInvestors = async () => {
      try {
        const investorData = await investorApi.getInvestors();
        setInvestors(investorData);
      } catch (err) {
        console.error('Error fetching investors:', err);
      }
    };
    fetchInvestors();
  }, []);

  return (
    <div className="investors-list-container">
      <h1 className="investors-list-title">Investors</h1>
      <table className="investors-table">
        <thead className="investors-table-head">
          <tr className="investors-table-row investors-table-header-row">
            <th className="investors-table-header">Id</th>
            <th className="investors-table-header">Name</th>
            <th className="investors-table-header">Type</th>
            <th className="investors-table-header">Date Added</th>
            <th className="investors-table-header">Country</th>
            <th className="investors-table-header">Total Commitments</th>
          </tr>
        </thead>
        <tbody className="investors-table-body">
          {investors.map((investor) => (
            <tr className="investors-table-row" key={investor.id}>
              <td className="investors-table-cell">{investor.id}</td>
              <td className="investors-table-cell">{investor.name}</td>
              <td className="investors-table-cell">{investor.type}</td>
              <td className="investors-table-cell">{investor.dateAdded}</td>
              <td className="investors-table-cell">{investor.country}</td>
              <td className="investors-table-cell">{investor.totalCommitments}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default InvestorsList;