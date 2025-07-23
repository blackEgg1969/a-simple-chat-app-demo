import { useState } from "react";
import "./App.css";

export default function RegisterPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirm, setConfirm] = useState("");
  const [message, setMessage] = useState("");

  const handleRegister = (e: React.FormEvent) => {
    e.preventDefault();
    if (!username || !password) {
      setMessage("Username and password are required.");
      return;
    }
    if (password !== confirm) {
      setMessage("Passwords do not match.");
      return;
    }
    // TODO: Call API to register user
    setMessage("Registered successfully (mock).");
  };

  return (
    <main>
      <h1>Register</h1>
      <form onSubmit={handleRegister}>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={e => setUsername(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <input
          type="password"
          placeholder="Confirm Password"
          value={confirm}
          onChange={e => setConfirm(e.target.value)}
        />
        <button type="submit">Register</button>
      </form>
      {message && <div>{message}</div>}
    </main>
  );
}