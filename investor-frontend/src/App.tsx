import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import InvestorsList from './components/InvestorsList';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/investors" element={<InvestorsList />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
