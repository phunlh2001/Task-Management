import React from 'react'
import './NavBar.css'

type Props = {}

const NavBar = (props: Props) => {
  return (
    <div className='navbar'>
        <div className="logo"></div>
        <div className="searchbox">
            <input type="text" />
        </div>
    </div>
  )
}

export default NavBar