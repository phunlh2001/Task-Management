import React, { useState } from 'react';
import { Routes, Route, useNavigate } from 'react-router-dom';
import './App.css';
import Login from './features/Login/index';

const App: React.FC = () => {

  const[token, setToken] = useState<boolean>(false);
  // Kiem tra token tra ve co dung hay khong neu sai tra lai o input
   if (!token) {  
    return <Login setToken={setToken}/>
  }
  return (
    <div className="App">

    </div>
  );
}

export default App;
