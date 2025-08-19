import axios from 'axios';
import { Investor, InvestorDetail } from '../types/types';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// object notation 
export const investorApi = {
  getInvestors: async (): Promise<Investor[]> => {
    const response = await api.get('/investors');
    return response.data;
  },

  getInvestor: async (id: number, assetClass?: string): Promise<InvestorDetail> => {
    const params = assetClass ? { assetClass } : {};
    const response = await api.get(`/investors/${id}`, { params });
    return response.data;
  }
};
