import { useState } from 'react';
import {
  Alert,
  Button,
  Card,
  CardContent,
  Grid2,
  List,
  ListItem,
  ListItemText,
  Stack,
  TextField,
  Typography
} from '@mui/material';
import { getForecast, getPromotionLift, optimizePricing } from '../services/analyticsApi';

export default function AiInsightsPage({ canWritePricing }) {
  const [sku, setSku] = useState('SKU-1001');
  const [forecast, setForecast] = useState([]);
  const [promoLift, setPromoLift] = useState(null);
  const [pricingResult, setPricingResult] = useState(null);
  const [error, setError] = useState('');

  const [pricingInput, setPricingInput] = useState({ cost: 1.2, currentPrice: 2.5, elasticity: -1.4, currentUnits: 120 });

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
    <Stack spacing={2}>
      <Typography variant="h5">AI Insights</Typography>
      {error && <Alert severity="error">{error}</Alert>}

      <Grid2 container spacing={2}>
        <Grid2 size={{ xs: 12, md: 6 }}>
          <Card sx={{ border: '1px solid', borderColor: 'divider' }}>
            <CardContent>
              <Typography variant="h6">Demand Forecast (4 weeks)</Typography>
              <Stack direction="row" spacing={1} sx={{ mt: 1.5 }}>
                <TextField label="SKU" value={sku} onChange={(e) => setSku(e.target.value)} fullWidth />
                <Button variant="contained" onClick={loadForecast}>Generate</Button>
              </Stack>
              <List dense>
                {forecast.map((item) => (
                  <ListItem key={item.weekStartDate} divider>
                    <ListItemText primary={item.weekStartDate} secondary={`${item.forecastUnits} units`} />
                  </ListItem>
                ))}
              </List>
            </CardContent>
          </Card>
        </Grid2>

        <Grid2 size={{ xs: 12, md: 6 }}>
          <Card sx={{ border: '1px solid', borderColor: 'divider' }}>
            <CardContent>
              <Stack direction="row" justifyContent="space-between" alignItems="center">
                <Typography variant="h6">Promotion Lift</Typography>
                <Button variant="contained" onClick={loadLift}>Calculate</Button>
              </Stack>
              {promoLift && (
                <List>
                  <ListItem><ListItemText primary="Baseline Revenue" secondary={`$${promoLift.baselineRevenue}`} /></ListItem>
                  <ListItem><ListItemText primary="Promo Revenue" secondary={`$${promoLift.promoRevenue}`} /></ListItem>
                  <ListItem><ListItemText primary="Lift Amount" secondary={`$${promoLift.liftAmount}`} /></ListItem>
                  <ListItem><ListItemText primary="Lift Percent" secondary={`${promoLift.liftPercent}%`} /></ListItem>
                </List>
              )}
            </CardContent>
          </Card>
        </Grid2>
      </Grid2>

      <Card sx={{ border: '1px solid', borderColor: 'divider' }}>
        <CardContent>
          <Typography variant="h6" sx={{ mb: 2 }}>Pricing Optimization</Typography>
          <Grid2 container spacing={2}>
            {Object.keys(pricingInput).map((key) => (
              <Grid2 key={key} size={{ xs: 12, sm: 6, md: 3 }}>
                <TextField
                  type="number"
                  label={key}
                  value={pricingInput[key]}
                  onChange={(e) => setPricingInput((prev) => ({ ...prev, [key]: Number(e.target.value) }))}
                  fullWidth
                />
              </Grid2>
            ))}
          </Grid2>
          <Stack direction={{ xs: 'column', sm: 'row' }} spacing={1.5} sx={{ mt: 2 }}>
            <Button variant="contained" onClick={runPricingOptimization} disabled={!canWritePricing}>
              Optimize Price
            </Button>
            {!canWritePricing && <Alert severity="warning">Admin/Analyst role required for pricing updates.</Alert>}
          </Stack>
          {pricingResult && (
            <Alert severity="info" sx={{ mt: 2 }}>
              Optimal Price: ${pricingResult.optimalPrice} | Projected Units: {pricingResult.projectedUnits} | Projected Revenue: ${pricingResult.projectedRevenue}
            </Alert>
          )}
        </CardContent>
      </Card>
    </Stack>
  );
}
