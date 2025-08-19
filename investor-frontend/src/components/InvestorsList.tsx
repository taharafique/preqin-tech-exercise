import React, { useState, useEffect } from 'react';
import { Investor } from '../types/types';
import { investorApi } from '../services/apis';

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
    <div>Investors</div>
  );
};

export default InvestorsList;