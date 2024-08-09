import React, { ReactNode } from 'react';
import Navbar from '../Navbar/Navbar';
import './Layout.css';

interface LayoutProps {
  children: ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <Navbar />
      <main className="main-content">{children}</main>
    </>
  );
};

export default Layout;
