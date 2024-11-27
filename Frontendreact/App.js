import React, { useState, useEffect } from "react";
import axios from "axios";

const API_URL = "https://localhost:44360/api/ToDo"; 

function App() {
    const [todos, setTodos] = useState([]);
    const [newTodo, setNewTodo] = useState("");

    useEffect(() => {
        axios.get(API_URL).then((response) => setTodos(response.data));
    }, []);

    const addTodo = () => {
        const todo = { title: newTodo, isComplete: false };
        axios.post(API_URL, todo).then((response) => {
            setTodos([...todos, response.data]);
            setNewTodo("");
        });
    };

    const toggleTodo = (id, isComplete) => {
        axios.put(`${API_URL}/${id}`, { isComplete: !isComplete })
            .then(() => {
                setTodos(
                    todos.map((todo) =>
                        todo.id === id ? { ...todo, isComplete: !isComplete } : todo
                    )
                );
            });
    };

    const deleteTodo = (id) => {
        axios.delete(`${API_URL}/${id}`).then(() => {
            setTodos(todos.filter((todo) => todo.id !== id));
        });
    };

    return (
        <div style={{ padding: "20px" }}>
            <h1>Todo List</h1>
            <div>
                <input
                    type="text"
                    value={newTodo}
                    onChange={(e) => setNewTodo(e.target.value)}
                />
                <button onClick={addTodo}>Add</button>
            </div>
            <ul>
                {todos.map((todo) => (
                    <li key={todo.id}>
                        <span
                            style={{
                                textDecoration: todo.isComplete ? "line-through" : "none",
                            }}
                        >
                            {todo.title}
                        </span>
                        <button onClick={() => toggleTodo(todo.id, todo.isComplete)}>
                            {todo.isComplete ? "Undo" : "Complete"}
                        </button>
                        <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
