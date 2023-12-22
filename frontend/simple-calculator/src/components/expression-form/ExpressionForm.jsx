import './ExpressionForm.css';
import React, { useState, useEffect } from 'react';

export default function ExpressionForm({expressionResult, onSubmitExpression})
{
    const [expression, setExpression] = useState("");
    const [result, setResult] = useState(null);
    const [ast, setAst] = useState(null);

    function handleExpressionChange(e)
    {
        // update state with new expression
        setExpression(e.target.value);
    }

    /**
     * handles user clicking on the submit button, which will make a rest call to back end to process the expression.
     */
    function handleSubmitClick()
    {
        onSubmitExpression(expression);
    }

    return (
        <div>
            <div className="ExpressionForm">
                <input type="text" onChange={(e) => handleExpressionChange(e)} />
                <button onClick={handleSubmitClick}>Submit</button>
            </div>
            <div>
                {expressionResult}
            </div>
        </div>
    );
}