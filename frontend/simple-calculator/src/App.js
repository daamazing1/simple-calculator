import { useState } from 'react';

import './App.css';
import './components/expression-form/ExpressionForm.jsx'
import ExpressionForm from './components/expression-form/ExpressionForm.jsx';
import AST from './components/ast/ast.jsx';

function App() {
  const [expressionResult, setExpressionResult] = useState(null);
  const [expressionAst, setExpressionAst] = useState(null);

  /**
   * 
   * @param {string} expression - string expression to send to backend
   */
  function fetchExpressionResult(expression)
  {
    fetch('http://localhost:8080/Calculate', {
            method: "POST", 
            mode: "cors",
            headers: {
                "Content-Type":"application/json",
                "Access-Control-Allow-Origin":"*"
            },
            body: JSON.stringify({ value: expression})
        })
        .then(response => response.json())
        .then((data) => {
            if(data.message === "OK")
            {
              setExpressionResult(data.result);
              setExpressionAst(data.ast);
            }
            else{
              setExpressionResult(data.message);
              setExpressionAst(null);
            }
        })
        .catch(error => console.error(error));
  }


  return (
    <div className="App">
      <header className="App-header">
        Simple Calculator
      </header>
      <ExpressionForm
        expressionResult={expressionResult}
        onSubmitExpression={fetchExpressionResult}/>
      {expressionAst !== null && (
        <AST
          expressionAst={expressionAst} />)
      }
    </div>
  );
}

export default App;
