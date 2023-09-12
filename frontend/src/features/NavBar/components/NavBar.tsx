import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './NavBar.css'
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons'
import Logo from "../../../images/images.png"

type Props = {}

const NavBar = (props: Props) => {

const searchIcon = <FontAwesomeIcon icon={faMagnifyingGlass} />

  return (
    <div className='navbar'>
      <div className="searchbox">
        <div className="searchbox__input">
        <input type="text" placeholder='Searching ......' />
        </div>
        <div className="searchicon">
        {searchIcon}
        </div>
      </div>
    </div>
  )
}

export default NavBar