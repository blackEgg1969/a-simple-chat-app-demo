import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import RegisterPage from './login/RegisterPage';
import ChatPage from './login/LoginPage'; // Assuming ChatPage is defined in a separate
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <nav>
        <Link to="/">Chat</Link> | <Link to="/register">Register</Link>
      </nav>
      <Routes>
        <Route path="/" element={<ChatPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App
