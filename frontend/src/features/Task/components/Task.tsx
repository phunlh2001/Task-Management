import React, { useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './Task.css'
import { faTrash } from '@fortawesome/free-solid-svg-icons'

type Props = {
  TaskArr:string[]
}

const Task:React.FC<Props> = ({TaskArr}) => {

  const [isGarbage, setIsGarbage] = useState<boolean>(false)

  const garbageIcon = <FontAwesomeIcon icon={faTrash} />

  return (
    <div className='tasklist'>
      <div className="tasklist__box">
        <div className="tasklist__add">
          <div className="tasklist__add__input">
            <input type="text" />
          </div>
          <button className='tasklist__add__btn'>Add</button>
        </div>
        <ul className="tasklist__list">
          {
            TaskArr.map(el => (
              <li onMouseEnter={() => setIsGarbage(!isGarbage)}
              onMouseLeave={() => setIsGarbage(!isGarbage)}>{el}{isGarbage && garbageIcon}</li>
            ))
          }
          {/* <li onMouseEnter={() => setIsGarbage(!isGarbage)}
            onMouseLeave={() => setIsGarbage(!isGarbage)}>hello{isGarbage && garbageIcon}</li>
          <li onMouseEnter={() => setIsGarbage(!isGarbage)}
            onMouseLeave={() => setIsGarbage(!isGarbage)}>hello{isGarbage && garbageIcon}</li>
             <li onMouseEnter={() => setIsGarbage(!isGarbage)}
            onMouseLeave={() => setIsGarbage(!isGarbage)}>hello{isGarbage && garbageIcon}</li> */}
        </ul>
      </div>
    </div>
  )
}

export default Task