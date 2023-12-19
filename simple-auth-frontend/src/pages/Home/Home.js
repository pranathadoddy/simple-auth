import axios from "axios";
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

function HomePage() {
  const [data, setData] = useState("default");
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      axios
        .get("https://localhost:7093/api/resources")
        .then((response) => {
          const data = response.data;
          setData(data);
        })
        .catch((err) => console.log(err));
    }
    else{
        navigate("/");
    }

      
  });

  const logout = () => {
    localStorage.removeItem("token");
    setData("default");
    navigate("/");
  };

  return <div style={{color: "#fff"}}>Home Page {data} <a href="#" style={{color: "#fff"}} onClick={logout}>Log Out</a> </div>;
}

export default HomePage;