import { useState } from 'react'
import './App.css'

function LoginPage() {
  // ...existing chat page code...
//   const [conn, setConnection] = useState();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const loginHandler = () => {
    if (username && password) {
      // TODO: check if username and password are valid by API
      console.log("Connected with:", { username, password });
    }
  }
  return (
    <main>
      <h1>Welcome to Simple ChatAPP</h1>
      <div>
        <form onSubmit={(e) => {
          e.preventDefault();
          loginHandler();
        }}>
          <input
            type="text"
            placeholder="Enter your username"
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="text"
            placeholder="Enter your password"
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="submit">Connect</button>
        </form>
      </div>
    </main>
  )
}



export default LoginPage