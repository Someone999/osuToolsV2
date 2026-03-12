using System;
using System.Collections.Generic;

namespace osuToolsV2.StoryBoard.Enums;

public class EnumTrie<TEnum> where TEnum : struct
{
    public EnumTrie(bool addEnumMembers = false)
    {
        if (addEnumMembers)
        {
            AddEnumMembers();
        }
    }
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord { get; set; }
        public TEnum? Value { get; set; }
    }

    private readonly TrieNode _root = new TrieNode();

    /// <summary>
    /// 向前缀树中添加一个字符串及其对应的枚举值
    /// </summary>
    /// <param name="key">要添加的字符串</param>
    /// <param name="value">对应的枚举值</param>
    public void Add(string key, TEnum value)
    {
        AddInternal(key.AsSpan(), value);
    }

    /// <summary>
    /// 内部方法，使用ReadOnlySpan&lt;char&gt;添加字符串及其对应的枚举值
    /// </summary>
    /// <param name="keySpan">要添加的字符串的ReadOnlySpan&lt;char&gt;</param>
    /// <param name="value">对应的枚举值</param>
    private void AddInternal(ReadOnlySpan<char> keySpan, TEnum value)
    {
        TrieNode current = _root;
        
        foreach (char c in keySpan)
        {
            if (!current.Children.TryGetValue(c, out TrieNode? child))
            {
                child = new TrieNode();
                current.Children[c] = child;
            }
            current = child;
        }
        
        current.IsEndOfWord = true;
        current.Value = value;
    }

    /// <summary>
    /// 查找前缀树中是否存在指定的字符串，并返回对应的枚举值
    /// </summary>
    /// <param name="key">要查找的字符串</param>
    /// <param name="value">找到的枚举值</param>
    /// <returns>是否找到指定的字符串</returns>
    public bool TryGetValue(string key, out TEnum value)
    {
        return TryGetValueInternal(key.AsSpan(), out value);
    }

    /// <summary>
    /// 内部方法，使用ReadOnlySpan&lt;char>查找字符串及其对应的枚举值
    /// </summary>
    /// <param name="keySpan">要查找的字符串的ReadOnlySpan&lt;char&gt;</param>
    /// <param name="value">找到的枚举值</param>
    /// <returns>是否找到指定的字符串</returns>
    private bool TryGetValueInternal(ReadOnlySpan<char> keySpan, out TEnum value)
    {
        TrieNode current = _root;
        
        foreach (char c in keySpan)
        {
            if (!current.Children.TryGetValue(c, out TrieNode? child))
            {
                value = default!;
                return false;
            }
            current = child;
        }
        
        if (current.IsEndOfWord && current.Value.HasValue)
        {
            value = current.Value.Value;
            return true;
        }
        
        value = default!;
        return false;
    }

    /// <summary>
    /// 检查前缀树中是否存在指定的字符串
    /// </summary>
    /// <param name="key">要检查的字符串</param>
    /// <returns>是否存在指定的字符串</returns>
    public bool ContainsKey(string key)
    {
        return ContainsKeyInternal(key.AsSpan());
    }

    /// <summary>
    /// 内部方法，使用ReadOnlySpan&lt;char>检查字符串是否存在
    /// </summary>
    /// <param name="keySpan">要检查的字符串的ReadOnlySpan&lt;char&gt;</param>
    /// <returns>是否存在指定的字符串</returns>
    private bool ContainsKeyInternal(ReadOnlySpan<char> keySpan)
    {
        TrieNode current = _root;
        
        foreach (char c in keySpan)
        {
            if (!current.Children.TryGetValue(c, out TrieNode? child))
            {
                return false;
            }
            current = child;
        }
        
        return current.IsEndOfWord;
    }

    /// <summary>
    /// 直接扫描整个枚举，将所有枚举成员添加到前缀树中
    /// </summary>
    public void AddEnumMembers()
    {
        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
        {
            string? name = value.ToString();
            if (name == null)
            {
                throw new InvalidOperationException();
            }
            
            Add(name, value);
        }
    }
}