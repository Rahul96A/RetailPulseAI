import { useState } from 'react';
import { Button, Card, CardContent, Grid2, Stack, Typography } from '@mui/material';
import { getAcv, getMarketShare, getPricingIndex } from '../services/analyticsApi';

const cardSx = {
  border: '1px solid',
  borderColor: 'divider',
  minHeight: 170,
  '&:hover': { transform: 'translateY(-2px)', boxShadow: 4 }
};

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
    <Stack spacing={2}>
      <Stack direction={{ xs: 'column', sm: 'row' }} justifyContent="space-between" alignItems={{ sm: 'center' }}>
        <Typography variant="h5">Market Analytics</Typography>
        <Button variant="contained" onClick={load}>Refresh</Button>
      </Stack>

      {error && <Typography color="error.main">{error}</Typography>}

      <Grid2 container spacing={2}>
        <Grid2 size={{ xs: 12, md: 4 }}>
          <Card sx={cardSx}><CardContent>
            <Typography color="text.secondary">Market Share</Typography>
            <Typography variant="h5" sx={{ mt: 1 }}>{marketShare ? `${marketShare.marketSharePercent}%` : '—'}</Typography>
            <Typography variant="body2" sx={{ mt: 1 }}>{marketShare ? `${marketShare.brand} / ${marketShare.category}` : ''}</Typography>
          </CardContent></Card>
        </Grid2>
        <Grid2 size={{ xs: 12, md: 4 }}>
          <Card sx={cardSx}><CardContent>
            <Typography color="text.secondary">ACV Distribution</Typography>
            <Typography variant="h5" sx={{ mt: 1 }}>{acv ? `${acv.weightedDistributionPercent}%` : '—'}</Typography>
            <Typography variant="body2" sx={{ mt: 1 }}>{acv ? `${acv.carryingStores} of ${acv.totalStores} stores` : ''}</Typography>
          </CardContent></Card>
        </Grid2>
        <Grid2 size={{ xs: 12, md: 4 }}>
          <Card sx={cardSx}><CardContent>
            <Typography color="text.secondary">Price Index</Typography>
            <Typography variant="h5" sx={{ mt: 1 }}>{pricingIndex ? `${pricingIndex.priceIndexPercent}%` : '—'}</Typography>
            <Typography variant="body2" sx={{ mt: 1 }}>
              {pricingIndex ? `Own: $${pricingIndex.ownAveragePrice} | Comp: $${pricingIndex.competitorAveragePrice}` : ''}
            </Typography>
          </CardContent></Card>
        </Grid2>
      </Grid2>
    </Stack>
  );
}
