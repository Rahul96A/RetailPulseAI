import { useState } from 'react';
import { getForecast, getPromotionLift, optimizePricing } from '../services/analyticsApi';

export default function AiInsightsPage({ canWritePricing }) {
  const [sku, setSku] = useState('SKU-1001');
  const [forecast, setForecast] = useState([]);
  const [promoLift, setPromoLift] = useState(null);
  const [pricingResult, setPricingResult] = useState(null);
  const [error, setError] = useState('');

  const [pricingInput, setPricingInput] = useState({
    cost: 1.2,
    currentPrice: 2.5,
    elasticity: -1.4,
    currentUnits: 120
  });

  const loadForecast = async () => {
    try {
      setError('');
      setForecast(await getForecast(sku));
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to load forecast');
    }
  };

  const loadLift = async () => {
    try {
      setError('');
      setPromoLift(await getPromotionLift());
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to load promotion lift');
    }
  };

  const runPricingOptimization = async () => {
    if (!canWritePricing) {
      setError('Current role is not authorized for pricing write actions.');
      return;
    }

    try {
      setError('');
      setPricingResult(await optimizePricing(pricingInput));
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to optimize pricing');
    }
  };

  return (
    <section className="panel">
      <h2>AI Insights</h2>
      {error && <p className="error">{error}</p>}

      <div className="split-grid">
        <article className="sub-panel">
          <h3>Demand Forecast (4 weeks)</h3>
          <div className="inline-row">
            <input value={sku} onChange={(event) => setSku(event.target.value)} placeholder="SKU" />
            <button type="button" className="primary-btn" onClick={loadForecast}>
              Generate
            </button>
          </div>
          <ul>
            {forecast.map((item) => (
              <li key={item.weekStartDate}>{item.weekStartDate}: {item.forecastUnits} units</li>
            ))}
          </ul>
        </article>

        <article className="sub-panel">
          <h3>Promotion Lift</h3>
          <button type="button" className="primary-btn" onClick={loadLift}>
            Calculate
          </button>
          {promoLift && (
            <ul>
              <li>Baseline Revenue: ${promoLift.baselineRevenue}</li>
              <li>Promo Revenue: ${promoLift.promoRevenue}</li>
              <li>Lift Amount: ${promoLift.liftAmount}</li>
              <li>Lift Percent: {promoLift.liftPercent}%</li>
            </ul>
          )}
        </article>
      </div>

      <article className="sub-panel">
        <h3>Pricing Optimization</h3>
        <div className="auth-grid">
          {Object.keys(pricingInput).map((key) => (
            <label key={key}>
              {key}
              <input
                type="number"
                value={pricingInput[key]}
                onChange={(event) =>
                  setPricingInput((prev) => ({ ...prev, [key]: Number(event.target.value) }))
                }
              />
            </label>
          ))}
        </div>
        <button type="button" className="primary-btn" onClick={runPricingOptimization}>
          Optimize Price
        </button>
        {pricingResult && (
          <p className="status">
            Optimal Price: ${pricingResult.optimalPrice} | Projected Units: {pricingResult.projectedUnits} |
            Projected Revenue: ${pricingResult.projectedRevenue}
          </p>
        )}
      </article>
    </section>
  );
}
