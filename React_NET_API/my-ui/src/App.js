import React from "react";
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import { Home } from './Home';
import { Department } from './Department';
import { Employee } from './Employee';
import { Navigation } from './Navigation';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <h3 className='m-3 d-flex justify-content-center'>
          React JS
        </h3>

        <Navigation/>

        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/department' element={<Department />} />
          <Route path='/employee' element={<Employee />} />
        </Routes>

      </div>
    </BrowserRouter>
  );
}

export default App;
