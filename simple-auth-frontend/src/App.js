import { BrowserRouter, Routes, Route } from "react-router-dom";

import "./App.css";

//pages

import LoginSignupPage from './pages/LoginSignup/LoginSignup';
import HomePage from './pages/Home/Home';


export default function App() {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<LoginSignupPage />} />
      <Route path="/Home" element={<HomePage />} />
    </Routes>
  </BrowserRouter>
  );
}