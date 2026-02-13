import { useState } from 'react';
import {
  Alert,
  Box,
  Button,
  Card,
  CardContent,
  Grid2,
  MenuItem,
  Stack,
  TextField,
  Typography
} from '@mui/material';
import { getToken } from '../services/analyticsApi';
import { setBearerToken } from '../services/apiClient';

const roles = ['Admin', 'Analyst', 'Executive', 'Viewer'];

export default function AuthPanel({ authState, onAuthChange }) {
  const [email, setEmail] = useState('analyst@demo-retail.com');
  const [role, setRole] = useState(authState.role);
  const [tenantId, setTenantId] = useState(authState.tenantId);
  const [tenantSlug, setTenantSlug] = useState(authState.tenantSlug);
  const [message, setMessage] = useState('Not authenticated');

  const requestToken = async () => {
    try {
      const data = await getToken({ email, role, tenantId, tenantSlug });
      setBearerToken(data.token);
      onAuthChange({ token: data.token, role, tenantId, tenantSlug });
      setMessage(`Authenticated as ${role}`);
    } catch (error) {
      setMessage(error?.response?.data?.error || 'Authentication failed');
    }
  };

  return (
    <Card elevation={0} sx={{ mb: 2, border: '1px solid', borderColor: 'divider' }}>
      <CardContent>
        <Stack direction={{ xs: 'column', sm: 'row' }} justifyContent="space-between" sx={{ mb: 2 }}>
          <Box>
            <Typography variant="h6">Session Context</Typography>
            <Typography variant="body2" color="text.secondary">
              Set user role and tenant to test authorization and isolation behavior.
            </Typography>
          </Box>
          <Button variant="contained" onClick={requestToken} aria-label="Generate JWT session">
            Generate JWT Session
          </Button>
        </Stack>

        <Grid2 container spacing={2}>
          <Grid2 size={{ xs: 12, md: 6 }}>
            <TextField fullWidth label="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
          </Grid2>
          <Grid2 size={{ xs: 12, md: 6 }}>
            <TextField select fullWidth label="Role" value={role} onChange={(e) => setRole(e.target.value)}>
              {roles.map((item) => (
                <MenuItem key={item} value={item}>{item}</MenuItem>
              ))}
            </TextField>
          </Grid2>
          <Grid2 size={{ xs: 12, md: 6 }}>
            <TextField fullWidth label="Tenant ID" value={tenantId} onChange={(e) => setTenantId(e.target.value)} />
          </Grid2>
          <Grid2 size={{ xs: 12, md: 6 }}>
            <TextField fullWidth label="Tenant Slug" value={tenantSlug} onChange={(e) => setTenantSlug(e.target.value)} />
          </Grid2>
        </Grid2>

        <Alert severity={message.includes('failed') ? 'error' : 'info'} sx={{ mt: 2 }}>
          {message}
        </Alert>
      </CardContent>
    </Card>
  );
}
