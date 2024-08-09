// TableView.tsx
import React from "react";
import { Link } from "react-router-dom";

interface Column {
  field: string;
  headerName: string;
}

interface LinkColumn {
  field: string;
  getUrl: (row: any) => string;
}

interface TableViewProps {
  columns: Column[];
  data: any[];
  linkColumn?: LinkColumn;
  rowsPerPage: number;
  currentPage: number;
  setCurrentPage: (page: number) => void;
  totalItems: number;
  totalPages: number;
}

const TableView: React.FC<TableViewProps> = ({ columns, data, linkColumn, rowsPerPage, currentPage, setCurrentPage, totalItems, totalPages }) => {
  
  
    const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };

  const firstItemIndex = (currentPage - 1) * rowsPerPage + 1;
  const lastItemIndex = Math.min(firstItemIndex + rowsPerPage - 1, totalItems);
  return (
    <>
      <div className="table-info mb-2">
        <span>Total items: {totalItems}</span>
        <span className="mx-3">|</span>
        <span>
          Showing {firstItemIndex}-{lastItemIndex} of {totalItems}
        </span>
      </div>
      <table className="table table-striped table-hover">
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={column.field}>{column.headerName}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((row, rowIndex) => (
            <tr key={rowIndex}>
              {columns.map((column) => {
                const cellValue = row[column.field];
                const isLinkColumn = linkColumn && column.field === linkColumn.field;
                return <td key={column.field}>{isLinkColumn ? <Link to={linkColumn.getUrl(row)}>{cellValue}</Link> : cellValue}</td>;
              })}
            </tr>
          ))}
        </tbody>
      </table>
      {totalPages > 1 && (
        <nav>
          <ul className="pagination">
            {Array.from({ length: totalPages }, (_, index) => index + 1).map((page) => (
              <li key={page} className={`page-item ${currentPage === page ? "active" : ""}`}>
                <button className="page-link" onClick={() => handlePageChange(page)}>
                  {page}
                </button>
              </li>
            ))}
          </ul>
        </nav>
      )}
    </>
  );
};

export default TableView;
