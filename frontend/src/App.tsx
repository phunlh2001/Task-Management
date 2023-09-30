import React, { useState } from 'react';
import { Routes, Route, useNavigate, Router } from 'react-router-dom';
import './App.css';
import Login from './features/Login/index';
import NavBar from './features/NavBar/components/NavBar';
import HomaPage from './features/HomePage/components/HomaPage';

const App: React.FC = () => {

  // const[token, setToken] = useState<boolean>(false);

  // Kiem tra token tra ve co dung hay khong neu sai tra lai o input
  // if (!token) {  
  //   return <Login setToken={setToken}/>
  // }
  return (
    <div className="App">
      <Routes>
        <Route path='/' element={<Login />}></Route>
        <Route path="/homepage" element={<><NavBar /><HomaPage /></>} />
      </Routes>
    </div>
  );
}

export default App;
