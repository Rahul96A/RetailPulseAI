import { useState } from 'react';
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
    <section className="panel auth-panel">
      <h2>Session Context</h2>
      <div className="auth-grid">
        <label>
          Email
          <input value={email} onChange={(event) => setEmail(event.target.value)} />
        </label>
        <label>
          Role
          <select value={role} onChange={(event) => setRole(event.target.value)}>
            {roles.map((item) => (
              <option key={item} value={item}>
                {item}
              </option>
            ))}
          </select>
        </label>
        <label>
          Tenant ID
          <input value={tenantId} onChange={(event) => setTenantId(event.target.value)} />
        </label>
        <label>
          Tenant Slug
          <input value={tenantSlug} onChange={(event) => setTenantSlug(event.target.value)} />
        </label>
      </div>
      <button type="button" onClick={requestToken} className="primary-btn">
        Generate JWT Session
      </button>
      <p className="status">{message}</p>
    </section>
  );
}
