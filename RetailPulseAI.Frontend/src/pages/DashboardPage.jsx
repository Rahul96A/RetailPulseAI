import { useState } from 'react';
import { getDashboardOverview, getSalesTrends } from '../services/analyticsApi';

export default function DashboardPage() {
  const [overview, setOverview] = useState(null);
  const [trends, setTrends] = useState([]);
  const [error, setError] = useState('');

  const loadDashboard = async () => {
    try {
      setError('');
      const [overviewData, trendsData] = await Promise.all([getDashboardOverview(), getSalesTrends()]);
      setOverview(overviewData);
      setTrends(trendsData.items || []);
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to load dashboard data');
    }
  };

  return (
    <section className="panel">
      <div className="panel-head">
        <h2>Dashboard Overview</h2>
        <button type="button" onClick={loadDashboard} className="primary-btn">
          Refresh
        </button>
      </div>

      {error && <p className="error">{error}</p>}

      {overview && (
        <div className="metric-grid">
          <article className="metric-card">
            <h3>Total Revenue</h3>
            <p>${overview.totalRevenue?.toFixed?.(2) ?? overview.totalRevenue}</p>
          </article>
          <article className="metric-card">
            <h3>Total Units</h3>
            <p>{overview.totalUnits}</p>
          </article>
          <article className="metric-card">
            <h3>Growth %</h3>
            <p>{overview.growthPercent}%</p>
          </article>
          <article className="metric-card">
            <h3>Market Share %</h3>
            <p>{overview.marketShare}%</p>
          </article>
        </div>
      )}

      <h3>Recent Sales Trends</h3>
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Revenue</th>
            <th>Units Sold</th>
          </tr>
        </thead>
        <tbody>
          {trends.map((row) => (
            <tr key={`${row.date}-${row.unitsSold}`}>
              <td>{row.date}</td>
              <td>${row.revenue}</td>
              <td>{row.unitsSold}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  );
}
