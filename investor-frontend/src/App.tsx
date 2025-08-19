import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import InvestorsList from './components/InvestorsList';
import InvestorInformation from './components/InvestorInformation';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/investors" element={<InvestorsList />} />
          <Route path="/investors/:id" element={<InvestorInformation />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
