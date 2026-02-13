import { useMemo, useState } from 'react';
import Header from './components/Header';
import AuthPanel from './components/AuthPanel';
import DashboardPage from './pages/DashboardPage';
import AnalyticsPage from './pages/AnalyticsPage';
import AiInsightsPage from './pages/AiInsightsPage';

const tabs = [
  { id: 'dashboard', label: 'Dashboard' },
  { id: 'analytics', label: 'Market Analytics' },
  { id: 'ai', label: 'AI Insights' }
];

export default function App() {
  const [activeTab, setActiveTab] = useState('dashboard');
  const [authState, setAuthState] = useState({
    token: '',
    role: 'Viewer',
    tenantId: '11111111-1111-1111-1111-111111111111',
    tenantSlug: 'demo-retail'
  });

  const canWritePricing = useMemo(() => ['Admin', 'Analyst'].includes(authState.role), [authState.role]);

  return (
    <div className="app-shell">
      <Header />
      <AuthPanel authState={authState} onAuthChange={setAuthState} />

      <div className="tab-bar">
        {tabs.map((tab) => (
          <button
            key={tab.id}
            className={`tab-button ${activeTab === tab.id ? 'active' : ''}`}
            onClick={() => setActiveTab(tab.id)}
            type="button"
          >
            {tab.label}
          </button>
        ))}
      </div>

      {activeTab === 'dashboard' && <DashboardPage authState={authState} />}
      {activeTab === 'analytics' && <AnalyticsPage authState={authState} />}
      {activeTab === 'ai' && <AiInsightsPage authState={authState} canWritePricing={canWritePricing} />}
    </div>
  );
}
