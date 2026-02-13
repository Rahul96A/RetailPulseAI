import { useState } from 'react';
import {
  Button,
  Card,
  CardContent,
  CircularProgress,
  Grid2,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography
} from '@mui/material';
import { getDashboardOverview, getSalesTrends } from '../services/analyticsApi';

const metricCardSx = {
  border: '1px solid',
  borderColor: 'divider',
  '&:hover': { transform: 'translateY(-2px)', boxShadow: 4 }
};

export default function DashboardPage() {
  const [overview, setOverview] = useState(null);
  const [trends, setTrends] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const loadDashboard = async () => {
    try {
      setLoading(true);
      setError('');
      const [overviewData, trendsData] = await Promise.all([getDashboardOverview(), getSalesTrends()]);
      setOverview(overviewData);
      setTrends(trendsData.items || []);
    } catch (apiError) {
      setError(apiError?.response?.data?.error || 'Unable to load dashboard data');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Stack spacing={2}>
      <Stack direction={{ xs: 'column', sm: 'row' }} justifyContent="space-between" alignItems={{ sm: 'center' }}>
        <Typography variant="h5">Dashboard Overview</Typography>
        <Button variant="contained" onClick={loadDashboard} disabled={loading}>
          {loading ? <CircularProgress size={18} color="inherit" /> : 'Refresh'}
        </Button>
      </Stack>

      {error && <Typography color="error.main">{error}</Typography>}

      <Grid2 container spacing={2}>
        {[
          { label: 'Total Revenue', value: overview ? `$${overview.totalRevenue?.toFixed?.(2) ?? overview.totalRevenue}` : '—' },
          { label: 'Total Units', value: overview?.totalUnits ?? '—' },
          { label: 'Growth %', value: overview ? `${overview.growthPercent}%` : '—' },
          { label: 'Market Share %', value: overview ? `${overview.marketShare}%` : '—' }
        ].map((metric) => (
          <Grid2 key={metric.label} size={{ xs: 12, sm: 6, lg: 3 }}>
            <Card sx={metricCardSx}>
              <CardContent>
                <Typography variant="body2" color="text.secondary">{metric.label}</Typography>
                <Typography variant="h6" sx={{ mt: 1 }}>{metric.value}</Typography>
              </CardContent>
            </Card>
          </Grid2>
        ))}
      </Grid2>

      <Card sx={{ border: '1px solid', borderColor: 'divider' }}>
        <CardContent>
          <Typography variant="h6" sx={{ mb: 1 }}>Recent Sales Trends</Typography>
          <TableContainer>
            <Table size="small" aria-label="sales trends table">
              <TableHead>
                <TableRow>
                  <TableCell>Date</TableCell>
                  <TableCell>Revenue</TableCell>
                  <TableCell>Units Sold</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {trends.map((row) => (
                  <TableRow key={`${row.date}-${row.unitsSold}`} hover>
                    <TableCell>{row.date}</TableCell>
                    <TableCell>${row.revenue}</TableCell>
                    <TableCell>{row.unitsSold}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </CardContent>
      </Card>
    </Stack>
  );
}
