import AppNav from './src/pages/Nav/AppNav'

import { AuthProvider } from './src/context/AuthContext';
import { LoadingProvider } from './src/context/LoadingContext';

export default function App() {
  return (
    <LoadingProvider>
      <AuthProvider>
        <AppNav>
        </AppNav>
    </AuthProvider>
    </LoadingProvider>
  );
}
