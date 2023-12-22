import {React, useState, useEffect} from 'react';
import {render} from 'react-dom';
import{ Stage, Layer, Circle, Text } from 'react-konva';
import Konva from 'konva';

export default function AST({expressionAst})
{
    const [expressionDepth, setExpressionDepth] = useState(0);

    /**
     * Returns the depth of the AST
     * @param {Object} node 
     * @param {number} depth 
     * @returns number
     */
    function getDepth(node, depth)
    {
        if(depth == null) depth = 0;
        depth++;

        // test for binary operation or number node
        if(node.$type === "SimpleCalculator.Parser.BinaryOperationNode, SimpleCalculator")
        {
            var left = getDepth(node.Left, depth);
            var right = getDepth(node.Right, depth);

            if(left>right)
                return left;
            else 
                return right;
        }
        else
        {
            return depth;
        }
    }

    useEffect(() =>
    {
        console.log("useEffect");
        setExpressionDepth(getDepth(expressionAst));
        console.log(expressionDepth);
    });

    return (
        <Stage width="500" height="500">
            <Layer>
                <Text text="Abstract Syntax Tree depth"/>
                <Circle
                    x={20}
                    y={20}
                    width={50}
                    height={50}
                    fill='green'
                    shadowBlur={5}
                />
            </Layer>
        </Stage>
    );
};