using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Modules.Inventories
{
    public class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        private Item[,] _itemGrid;

        private List<Item> _items = new List<Item>();

        private int _width, _height, _count;
        public int Width => _width;
        public int Height => _height;
        public int Count => _count;

        public Inventory(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException();

            _width = width;
            _height = height;
            _count = 0;
            _itemGrid = new Item[width, height];
        }

        public Inventory(
            int width,
            int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            for (int i = 0; i < items.Length; i++)
            {
                AddItem(items[i].Key, items[i].Value);
            }
        }

        public Inventory(
            int width,
            int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentNullException();

            foreach (Item item in items)
                AddItem(item);
        }

        public Inventory(
            int width,
            int height,
            IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (var item in items)
                AddItem(item.Key, item.Value);
        }

        public Inventory(
            int width,
            int height,
            IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (Item item in items)
                AddItem(item);
        }

        /// <summary>
        /// Creates new inventory 
        /// </summary>
        public Inventory(Inventory inventory)
        {
            _width = inventory.Width;
            _height = inventory.Height;
            _count = inventory.Count;
            _itemGrid = new Item[_width, _height];
            foreach (Item item in inventory)
                AddItem(item);
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(Item item, Vector2Int position)
        {
            return CanAddItem(item, position.x, position.y);
        }


        public bool CanAddItem(Item item, int startX, int startY)
        {
            if (!IsValidPosition(startX, startY) || this.Contains(item) || item == null)
                return false;

            if(item.Size.x <= 0 || item.Size.y <= 0)
                throw new ArgumentException();

            if(item.Size.x > _width || item.Size.y > _height)
                return false;

            Vector2Int size = item.Size;

            return IsFreeSpace(
                startX,
                startY,
                startX + size.x,
                startY + size.y
            );
        }

        /// <summary>
        /// Adds an item on a specified position
        /// </summary>
        public bool AddItem(Item item, Vector2Int position)
        {
            return AddItem(item, position.x, position.y);
        }

        private bool AddItemToGrid(Item item, int startX, int startY)
        {
            if (!CanAddItem(item, startX, startY))
                return false;

            Vector2Int itemSize = item.Size;

            for (int x = startX; x < startX + itemSize.x; x++)
            {
                for (int y = startY; y < startY + itemSize.y; y++)
                {
                    _itemGrid[x, y] = item;
                }
            }

            _count++;
            return true;
        }

        public bool AddItem(Item item, int startX, int startY)
        {
            if (!AddItemToGrid(item, startX, startY))
                return false;

            _items.Add(item);
            OnAdded?.Invoke(item, new Vector2Int(startX, startY));
            return true;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(Item item)
        {
            if (item == null)
                return false;

            if(Contains(item))
                return false;

            if (FindFreePosition(item, out var position))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(Item item)
        {
            if(item == null)
                return false;

            if (FindFreePosition(item, out var position))
            {
                return AddItem(item, position);
            }
            return false;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(Item item, out Vector2Int position)
        {
            if (item == null)
                throw new ArgumentNullException();

            return FindFreePosition(item.Size, out position);
        }

        public bool FindFreePosition(Vector2Int size, out Vector2Int position)
        {
            position = default;
           return FindFreePosition(size.x, size.y, out position);
        }

        public bool FindFreePosition(int sizeX, int sizeY, out Vector2Int position)
        {
            position = default;
            if (sizeX <= 0 || sizeY <= 0)
                throw new ArgumentException();

            if(sizeX > _width || sizeY > _height)
                return false;

            for (int y = 0; y <= _height - sizeY; y++)
            {
                for (int x = 0; x <= _width - sizeX; x++)
                {
                    if (IsFreeSpace(
                            x,
                            y,
                            x + sizeX,
                            y + sizeY))
                    {
                        position = new Vector2Int(x, y);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsFreeSpace(int startX, int startY, int endX, int endY)
        {
            if (startX < 0 || startY < 0 ||
                endX > _width || endY > _height ||
                endX < 0 || endY < 0 ||
                startX >=endX || startY >= endY)
            {
                return false;
            }

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if(_itemGrid[x, y] != null)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the specified element exists
        /// </summary>
        public bool Contains(Item item)
        {
            if (item == null)
                return false;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_itemGrid[x, y] == item)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the specified position is occupied
        /// </summary>
        public bool IsOccupied(Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        public bool IsOccupied(int x, int y)
        {
            return _itemGrid[x, y] != null && IsValidPosition(x,y);
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 &&
                   x < _width &&
                   y >= 0 &&
                   y < _height;
        }

        /// <summary>
        /// Checks if the specified position is free
        /// </summary>
        public bool IsFree(Vector2Int position)
        {
            return IsFree(position.x, position.y);
        }

        public bool IsFree(int x, int y)
        {
            return _itemGrid[x,y] == null;
        }

        /// <summary>
        /// Removes specified item
        /// </summary>
        public bool RemoveItem(Item item)
        {
            return RemoveItem(item, out _);
        }

        private bool RemoveItemFromGrid(Item item, out Vector2Int position)
        {
            position = default;

            if (!TryGetPositions(item, out Vector2Int[] positions))
                return false;

            position = positions[0];

            foreach (Vector2Int gridPosition in positions)
            {
                _itemGrid[gridPosition.x, gridPosition.y] = null;
            }

            _count--;
            return true;
        }

        public bool RemoveItem(Item item, out Vector2Int position)
        {
            if (!RemoveItemFromGrid(item, out position))
                return false;

            _items.Remove(item);
            OnRemoved?.Invoke(item, position);
            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(Vector2Int position)
        {
            return GetItem(position.x, position.y);
        }

        public Item GetItem(int x, int y)
        {
            if (!IsValidPosition(x, y))
                throw new IndexOutOfRangeException();

            return _itemGrid[x, y];
        }

        public bool TryGetItem(Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(int x, int y, out Item item)
        {
            if (!IsValidPosition(x, y))
            {
                item = null;
                return false;
            }
            item = GetItem(x, y);
            return item != null;
        }

        /// <summary>
        /// Returns positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(Item item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (!Contains(item))
                throw new KeyNotFoundException();

            if (!TryGetPositions(item, out Vector2Int[] positions))
                return Array.Empty<Vector2Int>();

            return positions;
        }

        public bool TryGetPositions(Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (!Contains(item))
            {
                return false;
            }

            if (item == null)
                return false;

            List<Vector2Int> temp = new List<Vector2Int>();

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_itemGrid[x, y] == item)
                        temp.Add(new Vector2Int(x, y));
                }
            }
            positions = temp.ToArray();
            return true;
        }

        /// <summary>
        /// Clears all items 
        /// </summary>
        public void Clear()
        {
            if (_count == 0)
                return;

            Array.Clear(_itemGrid, 0, _itemGrid.Length);

            _items.Clear();
            _count = 0;

            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;
            HashSet<Item> counted = new HashSet<Item>();

            foreach (var item in this)
            {
                if (item == null || counted.Contains(item))
                    continue;

                if (item.Name == name)
                {
                    count++;
                }
                counted.Add(item);
            }
            return count;
        }

        public bool MoveItem(Item item, Vector2Int position)
        {
            if(item == null)
                throw new ArgumentNullException();

            if (!Contains(item))
                return false;

            if (!TryGetPositions(item, out Vector2Int[] oldPositions))
                return false;

            Vector2Int oldPosition = oldPositions[0];

            if (oldPosition == position)
                return false;

            RemoveItemFromGrid(item, out _);
            if (IsFreeSpace(position.x, position.y, position.x + item.Size.x, position.y + item.Size.y))
            {
                AddItemToGrid(item, position.x, position.y);
                OnMoved?.Invoke(item, position);
                return true;
            }
            else
            {
                AddItem(item, oldPosition);
                return false;
            }
        }

        /// <summary>
        /// Rearranges an inventory space with max free slots 
        /// </summary>
        public void OptimizeSpace()
        {
            // var items = new List<Item>();
            //
            // foreach (Item item in _itemGrid)
            // {
            //     items.Add(item);
            //     Debug.Log(item.Name);
            // }
            // Debug.Log("===============================");
            var sortedItems = _items.ToArray();

            for (int i = 0; i < sortedItems.Length; i++)
            {
                var key = sortedItems[i];

                var j = i - 1;

                while (j >= 0 
                       && (sortedItems[j].Size.x * sortedItems[j].Size.y) < (key.Size.x *  key.Size.y)
                    ||
                    (j >= 0 
                     && sortedItems[j].Size.x * sortedItems[j].Size.y == key.Size.x * key.Size.y
                     && sortedItems[j].Size.y > key.Size.y
                    ))
                {
                    sortedItems[j+1] = sortedItems[j];
                    j = j - 1;
                }
                sortedItems[j + 1] = key;
            }

            Clear();

            foreach (Item item in sortedItems)
            {
                AddItem(item);
            }
        }
        /// <summary>
        /// Iterates by all items 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Item> GetEnumerator()
        {
            // var checkedItems = new HashSet<Item>();
            //
            // for (int y = 0; y < _height; y++)
            // {
            //     for (int x = 0; x < _width; x++)
            //     {
            //         Item item = _itemGrid[x, y];
            //
            //         if (item == null)
            //             continue;
            //
            //         if (checkedItems.Add(item))
            //             yield return item;
            //     }
            // }
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Copies items to a specified matrix
        /// </summary>
        public void CopyTo(Item[,] matrix)
        {
            Array.Copy(_itemGrid, matrix, _itemGrid.Length);
        }

        /// <summary>
        /// Returns an inventory matrix in string format
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Item item = _itemGrid[x, y];

                    builder.Append(item == null ? "." : item.Name);

                    if (y < _height - 1)
                        builder.Append(" ");
                }

                if (x < _width - 1)
                    builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
