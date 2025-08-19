export interface Investor {
  id: number;
  name: string;
  type: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  totalCommitments: number;
}

export interface Commitment {
  id: number;
  assetClass: string;
  amount: number;
  currency: string;
}

export interface InvestorDetail {
  id: number;
  name: string;
  type: string;
  country: string;
  dateAdded: string;
  lastUpdated: string;
  commitments: Commitment[];
}
