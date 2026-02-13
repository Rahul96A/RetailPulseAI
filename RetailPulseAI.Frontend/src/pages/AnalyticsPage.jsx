import { useState } from 'react';
import { getAcv, getMarketShare, getPricingIndex } from '../services/analyticsApi';

export default function AnalyticsPage() {
  const [marketShare, setMarketShare] = useState(null);
  const [acv, setAcv] = useState(null);
  const [pricingIndex, setPricingIndex] = useState(null);
  const [error, setError] = useState('');

  const load = async () => {
    try {
      setError('');
      const [market, acvData, pricing] = await Promise.all([getMarketShare(), getAcv(), getPricingIndex()]);
      setMarketShare(market);
      setAcv(acvData);
      setPricingIndex(pricing);
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to load market analytics');
    }
  };

  return (
    <section className="panel">
      <div className="panel-head">
        <h2>Market Analytics</h2>
        <button type="button" onClick={load} className="primary-btn">
          Refresh
        </button>
      </div>

      {error && <p className="error">{error}</p>}

      <div className="metric-grid">
        <article className="metric-card">
          <h3>Market Share</h3>
          <p>{marketShare ? `${marketShare.marketSharePercent}%` : '—'}</p>
          <small>
            {marketShare ? `${marketShare.brand} / ${marketShare.category}` : ''}
          </small>
        </article>

        <article className="metric-card">
          <h3>ACV Distribution</h3>
          <p>{acv ? `${acv.weightedDistributionPercent}%` : '—'}</p>
          <small>
            {acv ? `${acv.carryingStores} of ${acv.totalStores} stores` : ''}
          </small>
        </article>

        <article className="metric-card">
          <h3>Price Index</h3>
          <p>{pricingIndex ? `${pricingIndex.priceIndexPercent}%` : '—'}</p>
          <small>
            {pricingIndex ? `Own: $${pricingIndex.ownAveragePrice} | Comp: $${pricingIndex.competitorAveragePrice}` : ''}
          </small>
        </article>
      </div>
    </section>
  );
}
