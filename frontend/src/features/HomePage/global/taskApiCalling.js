import axios from "axios"
import { error } from "console";

export const taskGet = async (payload) => {
    await axios('http://localhost:5000/api/lists/getAll', payload)
        .then(res => {
            console.log(res.data);
        }).catch(error => {
            console.log(error);
        })
}