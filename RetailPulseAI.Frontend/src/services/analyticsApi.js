import apiClient from './apiClient';

const defaultDateRange = () => {
  const to = new Date();
  const from = new Date();
  from.setDate(to.getDate() - 30);

  return {
    fromDate: from.toISOString().slice(0, 10),
    toDate: to.toISOString().slice(0, 10)
  };
};

export async function getDashboardOverview() {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/dashboard/overview', { params: { fromDate, toDate } });
  return response.data;
}

export async function getSalesTrends(page = 1, pageSize = 30) {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/sales/trends', {
    params: { fromDate, toDate, page, pageSize }
  });
  return response.data;
}

export async function getMarketShare(brand = 'RetailPulse', category = 'Beverages') {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/marketshare', {
    params: { brand, category, fromDate, toDate }
  });
  return response.data;
}

export async function getAcv() {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/acv', { params: { fromDate, toDate } });
  return response.data;
}

export async function getPricingIndex() {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/pricing/index', { params: { fromDate, toDate } });
  return response.data;
}

export async function getForecast(sku = 'SKU-1001') {
  const response = await apiClient.get(`/api/forecast/${sku}`);
  return response.data;
}

export async function optimizePricing(payload) {
  const response = await apiClient.post('/api/pricing/optimize', payload);
  return response.data;
}

export async function getPromotionLift(productId = '') {
  const { fromDate, toDate } = defaultDateRange();
  const response = await apiClient.get('/api/promotions/lift', {
    params: {
      productId: productId || undefined,
      fromDate,
      toDate
    }
  });

  return response.data;
}

export async function getToken(payload) {
  const response = await apiClient.post('/api/auth/token', payload);
  return response.data;
}
