import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Layout from './components/common/Layout/Layout';
import InvestorsPage from './pages/InvestorsPage';
import CommitmentsPage from './pages/CommitmentsPage';
import NotFoundPage from './pages/NotFoundPage'; 




const App: React.FC = () => {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route index element={<InvestorsPage />} />
          <Route path="investors" element={<InvestorsPage />} />
          <Route path="investor/:investorId" element={<CommitmentsPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;

