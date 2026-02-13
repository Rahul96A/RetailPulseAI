import { useMemo, useState } from 'react';
import {
  AppBar,
  Box,
  Chip,
  Drawer,
  IconButton,
  List,
  ListItemButton,
  ListItemText,
  Stack,
  Toolbar,
  Typography,
  useMediaQuery
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import DashboardOutlinedIcon from '@mui/icons-material/DashboardOutlined';
import QueryStatsOutlinedIcon from '@mui/icons-material/QueryStatsOutlined';
import AutoGraphOutlinedIcon from '@mui/icons-material/AutoGraphOutlined';
import AuthPanel from './components/AuthPanel';
import DashboardPage from './pages/DashboardPage';
import AnalyticsPage from './pages/AnalyticsPage';
import AiInsightsPage from './pages/AiInsightsPage';

const drawerWidth = 260;
const tabs = [
  { id: 'dashboard', label: 'Dashboard', icon: <DashboardOutlinedIcon fontSize="small" /> },
  { id: 'analytics', label: 'Market Analytics', icon: <QueryStatsOutlinedIcon fontSize="small" /> },
  { id: 'ai', label: 'AI Insights', icon: <AutoGraphOutlinedIcon fontSize="small" /> }
];

export default function App() {
  const [activeTab, setActiveTab] = useState('dashboard');
  const [mobileOpen, setMobileOpen] = useState(false);
  const isDesktop = useMediaQuery('(min-width:900px)');

  const [authState, setAuthState] = useState({
    token: '',
    role: 'Viewer',
    tenantId: '11111111-1111-1111-1111-111111111111',
    tenantSlug: 'demo-retail'
  });

  const canWritePricing = useMemo(() => ['Admin', 'Analyst'].includes(authState.role), [authState.role]);

  const drawerContent = (
    <Box sx={{ p: 2 }}>
      <Typography variant="h6" sx={{ mb: 0.5 }}>RetailPulseAI</Typography>
      <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
        Modern retail intelligence workspace
      </Typography>
      <List>
        {tabs.map((tab) => (
          <ListItemButton
            key={tab.id}
            selected={tab.id === activeTab}
            onClick={() => {
              setActiveTab(tab.id);
              setMobileOpen(false);
            }}
            aria-label={`Open ${tab.label}`}
            sx={{ borderRadius: 2, mb: 0.5 }}
          >
            <Stack direction="row" spacing={1.5} alignItems="center">
              {tab.icon}
              <ListItemText primary={tab.label} />
            </Stack>
          </ListItemButton>
        ))}
      </List>
    </Box>
  );

  return (
    <Box sx={{ display: 'flex', minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar position="fixed" color="inherit" elevation={1} sx={{ zIndex: 1300 }}>
        <Toolbar>
          {!isDesktop && (
            <IconButton edge="start" onClick={() => setMobileOpen(true)} aria-label="Open navigation" sx={{ mr: 1 }}>
              <MenuIcon />
            </IconButton>
          )}
          <Box sx={{ flexGrow: 1 }}>
            <Typography variant="h6">RetailPulseAI Control Center</Typography>
            <Typography variant="caption" color="text.secondary">Multi-tenant analytics and AI decisioning</Typography>
          </Box>
          <Chip label={`Role: ${authState.role}`} color="primary" size="small" />
        </Toolbar>
      </AppBar>

      <Box component="nav" sx={{ width: { md: drawerWidth }, flexShrink: { md: 0 } }}>
        <Drawer
          variant={isDesktop ? 'permanent' : 'temporary'}
          open={isDesktop ? true : mobileOpen}
          onClose={() => setMobileOpen(false)}
          ModalProps={{ keepMounted: true }}
          sx={{
            '& .MuiDrawer-paper': {
              width: drawerWidth,
              boxSizing: 'border-box',
              borderRight: '1px solid #e7ebf0',
              mt: isDesktop ? 8 : 0
            }
          }}
        >
          {drawerContent}
        </Drawer>
      </Box>

      <Box component="main" sx={{ flexGrow: 1, p: { xs: 2, md: 3 }, mt: 9 }}>
        <AuthPanel authState={authState} onAuthChange={setAuthState} />
        {activeTab === 'dashboard' && <DashboardPage />}
        {activeTab === 'analytics' && <AnalyticsPage />}
        {activeTab === 'ai' && <AiInsightsPage canWritePricing={canWritePricing} />}
      </Box>
    </Box>
  );
}
